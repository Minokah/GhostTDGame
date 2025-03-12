using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Collections;
using System;

public class SpellManager : MonoBehaviour
{
    public bool isUsingSpell = false;

    public GameObject spellIcon1, spellIcon2, spellIcon3;
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
    private float coolDownUpgrade;
    private int freeSpellStacks;
    private Boolean eurekaUpgrade;

    UI UI;

    void Start()
    {
        coolDownUpgrade = 0f;
        freeSpellStacks = 0;
        eurekaUpgrade = false;
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
                }

                // Move the preview to follow the mouse (snapped to cell center)
                currentPreview.transform.position = cellCenter;

                // If user left-clicks, attempt to cast the spell
                if (Input.GetMouseButtonDown(0))
                {
                    // Instantiate the actual spell
                    Instantiate(spellPrefab, cellCenter, Quaternion.identity);
                    if (freeSpellStacks > 0)
                    {
                        freeSpellStacks = freeSpellStacks - 1;
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
                            StartCoroutine(startCoolDown(spellPrefab.GetComponent<BaseSpell>().spellId));
                        }
                    }
                    else { 
                        StartCoroutine(startCoolDown(spellPrefab.GetComponent<BaseSpell>().spellId)); 
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

    private IEnumerator startCoolDown(int spellId)
    {
        float waitTime = 15f - coolDownUpgrade;
        // TODO
        // When progression upgrades are in we need to use spellId to differentiate between wind spell which gets lower cooldown as an upgrade

        if (spellSlotId == 0)
        {
            spellIcon1.GetComponent<Button>().interactable = false;
            yield return new WaitForSeconds(waitTime);
            spellIcon1.GetComponent<Button>().interactable = true;
        }
        else if (spellSlotId == 1)
        {
            spellIcon2.GetComponent<Button>().interactable = false;
            yield return new WaitForSeconds(waitTime);
            spellIcon2.GetComponent<Button>().interactable = true;
        }
        else
        {
            spellIcon3.GetComponent<Button>().interactable = false;
            yield return new WaitForSeconds(waitTime);
            spellIcon3.GetComponent<Button>().interactable = true;
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
}