using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Collections;
using System;

public class SpellManager : MonoBehaviour
{
    public bool isUsingSpell = false;

    public GameObject vfxContainer;
    public GameObject spell1, spell2, spell3;

    // Map progression entries to VFX
    Dictionary<GameObject, SpellVFX> vfxMapping = new Dictionary<GameObject, SpellVFX>();

    GameObject spellPrefab;

    GameObject spellPreviewPrefab;

    public Tilemap grassTilemap;

    public LayerMask groundLayer;

    // The currently visible spell preview
    private GameObject currentPreview;

    // Used to diffrentiate the spell
    private int spellSlotId;

    // To Handle the Arcane Tower Bonuses
    private float coolDownUpgrade = 0f;
    private int freeSpellStacks = 0;
    private Boolean eurekaUpgrade = false;
	
	// To keep track of cooldown timers
	List<float> timerList = new List<float>{ 0f, 0f, 0f };
	List<float> originalTimeList = new List<float>{ 0f, 0f, 0f };
	
    // get all the upgrades for spells that we might have to apply
    public GameObject fireProgression, iceProgression, lightningProgression, windProgression, timeProgression;
    private ProgressionUpgrade[] fireUpgrades;
    private ProgressionUpgrade[] iceUpgrades;
    private ProgressionUpgrade[] lightningUpgrades;
    private ProgressionUpgrade[] windUpgrades;
    private ProgressionUpgrade[] timeUpgrades;

    Boolean chargeUpgrade1 = false, chargeUpgrade2 = false;

    Game Game;
    UI UI;

    void Start()
    {
        Game = Game.Get();
        UI = UI.Get();
        
        // Map Progression entity to VFX Object
        foreach (Transform t in vfxContainer.transform)
        {
            SpellVFX vfx = t.GetComponent<SpellVFX>();
            vfxMapping[vfx.entity] = vfx;
        }
    }

    void Update()
    {
		
		updateTimers();
        // Only do placement logic if we are currently in "placing" mode
        if (isUsingSpell)
        {
            // detect which spell slot was clicked
            if (spellSlotId == 0)
            {
                spellPrefab = vfxMapping[spell1].effect;
                spellPreviewPrefab = vfxMapping[spell1].preview;
            }
            else if (spellSlotId == 1)
            {
                spellPrefab = vfxMapping[spell2].effect;
                spellPreviewPrefab = vfxMapping[spell2].preview;
            }
            else
            {
                spellPrefab = vfxMapping[spell3].effect;
                spellPreviewPrefab = vfxMapping[spell3].preview;
            }

            // If right clicked, cancel placement
            if (Input.GetMouseButtonUp(1))
            {
                Stop();
                return;
            }

            // Ray from camera to mouse
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // If the ray hits the ground plane/tilemap within 1000 units
            if (Physics.Raycast(ray, out hit, 1000f, groundLayer))
            {
                // The world position where the raycast hit
                Vector3 hitPoint = hit.point;

                // Convert that to the tilemap cell position
                Vector3Int cellPos = grassTilemap.WorldToCell(hitPoint);

                // Get the center of the cell in world space
                Vector3 cellCenter = grassTilemap.GetCellCenterWorld(cellPos);

                // If we haven't created a preview object yet, instantiate it
                if (currentPreview == null)
                {
                    currentPreview = Instantiate(spellPreviewPrefab);
                    // apply any range upgrades to the preview as well
                    if (currentPreview.GetComponent<BaseSpell>().spellId == 0)
                    {
                        fireUpgrades = fireProgression.GetComponents<ProgressionUpgrade>();
                        foreach (ProgressionUpgrade upgrade in fireUpgrades)
                        {
                            if (upgrade.id == "Radius")
                            {
                                currentPreview.GetComponent<BaseSpell>().setHigherRadius(upgrade.returnUpgradeStat());
                            }
                        }
                    }
                    else if (currentPreview.GetComponent<BaseSpell>().spellId == 1)
                    {
                        iceUpgrades = iceProgression.GetComponents<ProgressionUpgrade>();
                        foreach (ProgressionUpgrade upgrade in iceUpgrades)
                        {
                            if (upgrade.id == "Radius")
                            {
                                currentPreview.GetComponent<BaseSpell>().setHigherRadius(upgrade.returnUpgradeStat());
                            }
                        }
                    }
                    else if (currentPreview.GetComponent<BaseSpell>().spellId == 2)
                    {
                        lightningUpgrades = lightningProgression.GetComponents<ProgressionUpgrade>();
                        foreach (ProgressionUpgrade upgrade in lightningUpgrades)
                        {
                            if (upgrade.id == "Radius")
                            {
                                currentPreview.GetComponent<BaseSpell>().setHigherRadius(upgrade.returnUpgradeStat());
                            }
                        }
                    }
                    else if (currentPreview.GetComponent<BaseSpell>().spellId == 3)
                    {
                        windUpgrades = windProgression.GetComponents<ProgressionUpgrade>();
                        foreach (ProgressionUpgrade upgrade in windUpgrades)
                        {
                            if (upgrade.id == "Radius")
                            {
                                currentPreview.GetComponent<BaseSpell>().setHigherRadius(upgrade.returnUpgradeStat());
                            }
                        }
                    }
                }

                // Move the preview to follow the mouse (snapped to cell center)
                currentPreview.transform.position = cellCenter;

                // If user left-clicks, attempt to cast the spell
                if (Input.GetMouseButtonDown(0))
                {
                    // Instantiate the actual spell
                    GameObject newSpell = Instantiate(spellPrefab, cellCenter, Quaternion.identity);
                    Game.StatisticsManager.IncrementStatistics("spellsCasted");
                    
                    if (newSpell.GetComponent<BaseSpell>().spellId == 0)
                    {
                        Game.StatisticsManager.IncrementStatistics("fireCasts");
                        fireUpgrades = fireProgression.GetComponents<ProgressionUpgrade>();
                        foreach (ProgressionUpgrade upgrade in fireUpgrades)
                        {
                            if (upgrade.id == "Radius")
                            {
                                newSpell.GetComponent<FireSpell>().setHigherRadius(upgrade.returnUpgradeStat());
                            }
                            if (upgrade.id == "Damage")
                            {
                                newSpell.GetComponent<FireSpell>().setHigherDamage(upgrade.returnUpgradeStat());
                            }

                            if (upgrade.id == "Napalm" & upgrade.level == 1)
                            {
                                newSpell.GetComponent<FireSpell>().setNapalm();
                            }
                            if (upgrade.id == "Inferno" & upgrade.level == 1)
                            {
                                newSpell.GetComponent<FireSpell>().setHigherRadius(0.5f);
                            }
                        }
                    }
                    else if (newSpell.GetComponent<BaseSpell>().spellId == 1)
                    {
                        Game.StatisticsManager.IncrementStatistics("iceCasts");
                        iceUpgrades = iceProgression.GetComponents<ProgressionUpgrade>();
                        foreach (ProgressionUpgrade upgrade in iceUpgrades)
                        {
                            if (upgrade.id == "Radius")
                            {
                                newSpell.GetComponent<IceSpell>().setHigherRadius(upgrade.returnUpgradeStat());
                            }
                            if (upgrade.id == "Speed")
                            {
                                newSpell.GetComponent<IceSpell>().setMoreSlow(upgrade.returnUpgradeStat());
                            }

                            if (upgrade.id == "Freeze" & upgrade.level == 1)
                            {
                                newSpell.GetComponent<IceSpell>().setFreeze();
                            }
                            if (upgrade.id == "Chill" & upgrade.level == 1)
                            {
                                newSpell.GetComponent<IceSpell>().setChill();
                            }
                        }
                    }
                    else if (newSpell.GetComponent<BaseSpell>().spellId == 2)
                    {
                        Game.StatisticsManager.IncrementStatistics("lightningCasts");
                        lightningUpgrades = lightningProgression.GetComponents<ProgressionUpgrade>();
                        foreach (ProgressionUpgrade upgrade in lightningUpgrades)
                        {
                            if (upgrade.id == "Radius")
                            {
                                newSpell.GetComponent<LightningSpell>().setHigherRadius(upgrade.returnUpgradeStat());
                            }
                            if (upgrade.id == "Damage")
                            {
                                newSpell.GetComponent<LightningSpell>().setHigherDamage(upgrade.returnUpgradeStat());
                            }

                            if (upgrade.id == "Storm" & upgrade.level == 1)
                            {
                                chargeUpgrade2 = true;
                                newSpell.GetComponent<LightningSpell>().setStorm();
                            }
                            if (upgrade.id == "Charged" & upgrade.level == 1)
                            {
                                chargeUpgrade1 = true;
                            }
                        }
                    }
                    else if (newSpell.GetComponent<BaseSpell>().spellId == 3)
                    {
                        Game.StatisticsManager.IncrementStatistics("windCasts");
                        windUpgrades = windProgression.GetComponents<ProgressionUpgrade>();
                        foreach (ProgressionUpgrade upgrade in windUpgrades)
                        {
                            if (upgrade.id == "Radius")
                            {
                                newSpell.GetComponent<WindSpell>().setHigherRadius(upgrade.returnUpgradeStat());
                            }
                            if (upgrade.id == "Distance")
                            {
                                newSpell.GetComponent<WindSpell>().setHigherKnockback((int)upgrade.returnUpgradeStat());
                            }

                            if (upgrade.id == "Hurricane" & upgrade.level == 1)
                            {
                                newSpell.GetComponent<WindSpell>().setHigherRadius(0.5f);
                            }
                            if (upgrade.id == "BackToSender" & upgrade.level == 1)
                            {
                                newSpell.GetComponent<WindSpell>().setKickBack();
                            }
                        }
                    }
                    else if (newSpell.GetComponent<BaseSpell>().spellId == 4)
                    {
                        Game.StatisticsManager.IncrementStatistics("timeCasts");
                        timeUpgrades = timeProgression.GetComponents<ProgressionUpgrade>();
                        foreach (ProgressionUpgrade upgrade in timeUpgrades)
                        {
                            if (upgrade.id == "Speed")
                            {
                                newSpell.GetComponent<TimeSpell>().setMoreSlow(upgrade.returnUpgradeStat());
                            }
                            if (upgrade.id == "Duration")
                            {
                                newSpell.GetComponent<TimeSpell>().setHigherDuration((int)upgrade.returnUpgradeStat());
                            }

                            if (upgrade.id == "Rewind" & upgrade.level == 1)
                            {
                                newSpell.GetComponent<TimeSpell>().setRewind();
                            }
                            if (upgrade.id == "Freeze" & upgrade.level == 1)
                            {
                                newSpell.GetComponent<TimeSpell>().setFreeze();
                            }
                        }
                    }

                    if (freeSpellStacks > 0)
                    {
                        freeSpellStacks = freeSpellStacks - 1;
						UI.Spellbar.SetEureka(freeSpellStacks);
                    }
                    else if (eurekaUpgrade == true)
                    {
                        float randomValue = UnityEngine.Random.Range(0f, 1f);
                        if (randomValue >= 0.75f)
                        {
                            // do nothing
                        }
                        else
                        {
                            startCoolDown(spellSlotId, newSpell.GetComponent<BaseSpell>().spellId);
                        }
                    }
                    else
                    {
                        startCoolDown(spellSlotId, newSpell.GetComponent<BaseSpell>().spellId);
                    }

                    // If you only want to place one tower per "mode," exit placing mode
                    Destroy(currentPreview);
                    currentPreview = null;
                    isUsingSpell = false;
                    UI.Castbar.Hide();
                }
            }
            else
            {
                // If raycast hits nothing, either hide the preview or move it offscreen
                if (currentPreview != null)
                {
                    // Move it far away or disable it
                    currentPreview.transform.position = new Vector3(9999f, 9999f, 9999f);
                }
            }
        }
        else
        {
            // If we are NOT casting a spell, make sure no preview object remains
            if (currentPreview != null)
            {
                Destroy(currentPreview);
                currentPreview = null;
            }
        }
    }

    // Call this from a button to toggle placement mode on/off
    public void Place(int spell)
    {
        isUsingSpell = true;
        spellSlotId = spell;
    }

    public void Stop() {
        if (!isUsingSpell) return;

        isUsingSpell = false;
        UI.Castbar.Hide();

        // If turning off, destroy any existing preview
        if (!isUsingSpell && currentPreview != null)
        {
            Destroy(currentPreview);
            currentPreview = null;
        }
    }

    private void startCoolDown(int slotId, int spellId)
    {
        float waitTime = 12f - coolDownUpgrade;
        if (chargeUpgrade1 == true & spellId == 2)
        {
            waitTime = waitTime - 2f;
        }
        else if (chargeUpgrade2 == true & spellId == 2)
        {
            waitTime = waitTime - 4f;
        }
		
        if (slotId == 0)
        {
            UI.Spellbar.SetSpellState(1, false);
			timerList[0] = waitTime;
			originalTimeList[0] = waitTime;
        }
        else if (slotId == 1)
        {
            UI.Spellbar.SetSpellState(2, false);
			timerList[1] = waitTime;
			originalTimeList[1] = waitTime;
        }
        else
        {
            UI.Spellbar.SetSpellState(3, false);
			timerList[2] = waitTime;
			originalTimeList[2] = waitTime;
        }
    }
	
	private void updateTimers()
	{
		if (timerList[0] > 0f){
			timerList[0] = timerList[0] - Time.deltaTime;
			UI.Spellbar.SetSpellCooldown(1, timerList[0]/originalTimeList[0]);
		}
		else{
			UI.Spellbar.SetSpellState(1, true);
		}
		
		if (timerList[1] > 0f){
			timerList[1] = timerList[1] - Time.deltaTime;
			UI.Spellbar.SetSpellCooldown(2, timerList[1]/originalTimeList[1]);
		}
		else{
			UI.Spellbar.SetSpellState(2, true);
		}
		
		if (timerList[2] > 0f){
			timerList[2] = timerList[2] - Time.deltaTime;
			UI.Spellbar.SetSpellCooldown(3, timerList[2]/originalTimeList[2]);
		}
		else{
			UI.Spellbar.SetSpellState(3, true);
		}
	}

    public void arcaneTowerCooldownEffect(float cooldown)
    {
        coolDownUpgrade = coolDownUpgrade + cooldown;
    }

    public void arcaneTowerFreeEffect(int freeSpells)
    {
        freeSpellStacks = freeSpellStacks + freeSpells;
    }

    public void arcaneTowerEurekaEffect(Boolean eureka)
    {
        eurekaUpgrade = eureka;
    }
	
	public void resetSpellManager()
    {
        StopAllCoroutines();

        UI.Spellbar.SetSpellState(1, true);
        UI.Spellbar.SetSpellState(2, true);
        UI.Spellbar.SetSpellState(3, true);
		
		timerList[0] = 0f;
		timerList[1] = 0f;
		timerList[2] = 0f;
		originalTimeList[0] = 0f;
		originalTimeList[1] = 0f;
		originalTimeList[2] = 0f;

        coolDownUpgrade = 0f;
		freeSpellStacks = 0;
		UI.Spellbar.SetEureka(0);
		eurekaUpgrade = false;
		
		isUsingSpell = false;
		
		UI.Spellbar.ResetSpellCooldown();
        UI.Castbar.Hide();
		
		if (currentPreview != null)
        {
            Destroy(currentPreview);
            currentPreview = null;
        }
    }
}