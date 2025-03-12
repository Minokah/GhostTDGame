using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using TMPro;
using System;

public class TowerPlacementManager : MonoBehaviour
{
    [Header("Placement Settings")]
    public bool isPlacingTower = false;

    [Tooltip("Refer to the buttons used to select towers to build")]
    public GameObject boltIcon, bombIcon, sniperIcon, bubbleIcon, gateIcon, gathererIcon, arcaneIcon;

    [Tooltip("Refer to the text in the building towers menu")]
    public TMP_Text boltText, bombText, sniperText, bubbleText, gateText, gathererText, arcaneText;

    [Tooltip("All possible real tower prefabs")]
    public GameObject potentialPrefab0, potentialPrefab1, potentialPrefab2, potentialPrefab3, potentialPrefab4, potentialPrefab5, potentialPrefab6;

    [Tooltip("All possible prefab outlines")]
    public GameObject potentialPreviewPrefab0, potentialPreviewPrefab1, potentialPreviewPrefab2, potentialPreviewPrefab3, potentialPreviewPrefab4, potentialPreviewPrefab5, potentialPreviewPrefab6;

    [Tooltip("Selected Real tower prefab (e.g., cylinder)")]
    GameObject towerPrefab;

    [Tooltip("Selected Ghost/outline tower prefab (semi-transparent or outlined material)")]
    GameObject towerPreviewPrefab;

    [Tooltip("Tilemap containing grass tiles where towers can be placed")]
    public Tilemap grassTilemap;

    [Tooltip("LayerMask for the ground plane or tilemap collider")]
    public LayerMask groundLayer;

    // Keep track of which cells already have a tower placed
    private HashSet<Vector3Int> occupiedCells = new HashSet<Vector3Int>();

    // The currently visible tower preview
    private GameObject currentPreview;

    // Used to diffrentiate what tower we are placing
    private int towerId;

    // Used to connect the money mangment object to the money gatherer tower
    public StatisticsManager masterMoneyManager;

    // keeps track if the max 1 spirit gather, gate, and/or arcane tower has been built
    Boolean gateBuilt = false, gatherBuilt = false, arcaneBuilt = false;

    // get all the upgrades for towers that we might have to apply
    public GameObject boltProgression, bombProgression, sniperProgression, bubbleProgression, gateProgression, gathererProgression, arcaneProgression;
    private ProgressionUpgrade[] boltUpgrades;
    private ProgressionUpgrade[] bombUpgrades;
    private ProgressionUpgrade[] sniperUpgrades;
    private ProgressionUpgrade[] bubbleUpgrades;
    private ProgressionUpgrade[] gateUpgrades;
    private ProgressionUpgrade[] gathererUpgrades;
    private ProgressionUpgrade[] arcaneUpgrades;
    UI UI;

    void Start()
    {
        UI = UI.Get();
    }

    void Update()
    {
        if (masterMoneyManager != null)
        {
            // prevent players from building towers if they lack money
            updateBoltStatus();
            updateBombStatus();
            updateSniperStatus();
            updateBubbleStatus();
            // no need to check these towers if players already cannot build(more) of them
            if (gateBuilt == false)
            {
                updateGateStatus();
            }
            if (gatherBuilt == false)
            {
                updateGatherStatus();
            }
            if (arcaneBuilt == false)
            {
                updateArcaneStatus();
            }
        }

        // Only do placement logic if we are currently in "placing" mode
        if (isPlacingTower)
        {

            if (towerId == 0)
            {
                towerPrefab = potentialPrefab0;
                towerPreviewPrefab = potentialPreviewPrefab0;
            }
            else if (towerId == 1)
            {
                towerPrefab = potentialPrefab1;
                towerPreviewPrefab = potentialPreviewPrefab1;
            }
            else if (towerId == 2)
            {
                towerPrefab = potentialPrefab2;
                towerPreviewPrefab = potentialPreviewPrefab2;
            }
            else if (towerId == 3)
            {
                towerPrefab = potentialPrefab3;
                towerPreviewPrefab = potentialPreviewPrefab3;
            }
            else if (towerId == 4)
            {
                towerPrefab = potentialPrefab4;
                towerPreviewPrefab = potentialPreviewPrefab4;
            }
            else if (towerId == 5)
            {
                towerPrefab = potentialPrefab5;
                towerPreviewPrefab = potentialPreviewPrefab5;
            }
            else
            {
                towerPrefab = potentialPrefab6;
                towerPreviewPrefab = potentialPreviewPrefab6;
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
                    currentPreview = Instantiate(towerPreviewPrefab);
                }

                // Move the preview to follow the mouse (snapped to cell center)
                currentPreview.transform.position = cellCenter;

                // Check if there's a valid grass tile at this cell
                TileBase tile = grassTilemap.GetTile(cellPos);
                bool isGrassTile = (tile != null); // or compare tile name, etc.

                // Check if already occupied
                bool isOccupied = occupiedCells.Contains(cellPos);

                // Determine overall validity
                bool validPlacement = isGrassTile && !isOccupied;

                // OPTIONAL: Change the preview color based on validity
                Renderer previewRenderer = currentPreview.GetComponentInChildren<Renderer>();
                if (previewRenderer != null)
                {
                    previewRenderer.material.color = validPlacement ? Color.green : Color.red;
                }

                // If user left-clicks, attempt to place the real tower
                if (Input.GetMouseButtonDown(0))
                {
                    if (validPlacement)
                    {
                        // Instantiate the actual tower
                        GameObject newTower = Instantiate(towerPrefab, cellCenter, Quaternion.identity);
                        if (towerId == 0)
                        {
                            masterMoneyManager.addMoney(-15);

                            boltUpgrades = boltProgression.GetComponents<ProgressionUpgrade>();
                            foreach (ProgressionUpgrade upgrade in boltUpgrades)
                            {
                                if (upgrade.id == "Damage")
                                {
                                    newTower.GetComponent<Tower>().setHigherDamage(upgrade.returnUpgradeStat());
                                }
                                if (upgrade.id == "FireRate")
                                {
                                    newTower.GetComponent<Tower>().setAttackSpeed(upgrade.returnUpgradeStat());
                                }

                                if (upgrade.id == "Fast" & upgrade.level == 1)
                                {
                                    newTower.GetComponent<Tower>().setSpecialOne();
                                }
                                if (upgrade.id == "Stun" & upgrade.level == 1)
                                {
                                    newTower.GetComponent<Tower>().setSpecialTwo();
                                }
                            }
                        }
                        else if (towerId == 1)
                        {
                            masterMoneyManager.addMoney(-20);

                            bombUpgrades = bombProgression.GetComponents<ProgressionUpgrade>();
                            foreach (ProgressionUpgrade upgrade in bombUpgrades)
                            {
                                if (upgrade.id == "Damage")
                                {
                                    newTower.GetComponent<Tower>().setHigherDamage(upgrade.returnUpgradeStat());
                                }
                                if (upgrade.id == "Range")
                                {
                                    newTower.GetComponent<Tower>().setAttackRange(upgrade.returnUpgradeStat());
                                }

                                if (upgrade.id == "Napalm" & upgrade.level == 1)
                                {
                                    newTower.GetComponent<Tower>().setSpecialOne();
                                }
                                if (upgrade.id == "Fast" & upgrade.level == 1)
                                {
                                    newTower.GetComponent<Tower>().setSpecialTwo();
                                }
                            }
                        }

                        else if (towerId == 2)
                        {
                            masterMoneyManager.addMoney(-20);

                            sniperUpgrades = sniperProgression.GetComponents<ProgressionUpgrade>();
                            foreach (ProgressionUpgrade upgrade in sniperUpgrades)
                            {
                                if (upgrade.id == "Range")
                                {
                                    newTower.GetComponent<Tower>().setAttackRange(upgrade.returnUpgradeStat());
                                }
                                if (upgrade.id == "FireRate")
                                {
                                    newTower.GetComponent<Tower>().setAttackSpeed(upgrade.returnUpgradeStat());
                                }
                            }
                        }

                        else if (towerId == 3)
                        {
                            masterMoneyManager.addMoney(-25);

                            bubbleUpgrades = bubbleProgression.GetComponents<ProgressionUpgrade>();
                            foreach (ProgressionUpgrade upgrade in bubbleUpgrades)
                            {
                                if (upgrade.id == "SlowRate")
                                {
                                    newTower.GetComponent<BubbleTower>().setSlowRate(upgrade.returnUpgradeStat());
                                }
                                if (upgrade.id == "Range")
                                {
                                    newTower.GetComponent<BubbleTower>().setEffectRange(upgrade.returnUpgradeStat());
                                }
                            }
                        }

                        else if (towerId == 4)
                        {
                            masterMoneyManager.addMoney(-50);
                            gateIcon.GetComponent<Button>().interactable = false;
                            gateBuilt = true;
                            newTower.GetComponent<GateTower>().reduceSpawn(1);
                            newTower.GetComponent<GateTower>().increaseBreaks(0.25f);

                            gateUpgrades = gateProgression.GetComponents<ProgressionUpgrade>();
                            foreach (ProgressionUpgrade upgrade in gateUpgrades)
                            {
                                if (upgrade.id == "SpawnRate")
                                {
                                    newTower.GetComponent<GateTower>().reduceSpawn((int)upgrade.returnUpgradeStat());
                                }
                                if (upgrade.id == "SpawnDelay")
                                {
                                    newTower.GetComponent<GateTower>().increaseBreaks(upgrade.returnUpgradeStat());
                                }
                            }
                        }

                        else if (towerId == 5)
                        {
                            masterMoneyManager.addMoney(-50);
                            newTower.GetComponent<GatherTower>().moneyManager = masterMoneyManager;
                            gathererIcon.GetComponent<Button>().interactable = false;
                            gatherBuilt = true;

                            gathererUpgrades = gathererProgression.GetComponents<ProgressionUpgrade>();
                            foreach (ProgressionUpgrade upgrade in gathererUpgrades)
                            {
                                if (upgrade.id == "GainAmount")
                                {
                                    newTower.GetComponent<GatherTower>().moreMoneyGeneration((int)upgrade.returnUpgradeStat());
                                }
                                if (upgrade.id == "GainRate")
                                {
                                    newTower.GetComponent<GatherTower>().fasterMoneyGeneration(upgrade.returnUpgradeStat());
                                }
                            }
                        }

                        else if (towerId == 6)
                        {
                            masterMoneyManager.addMoney(-50);
                            arcaneIcon.GetComponent<Button>().interactable = false;
                            arcaneBuilt = true;
                            newTower.GetComponent<ArcaneTower>().cooldownEffect(1.5f);
                            newTower.GetComponent<ArcaneTower>().freeEffect(0);
                            newTower.GetComponent<ArcaneTower>().eurekaEffect(false);

                            arcaneUpgrades = arcaneProgression.GetComponents<ProgressionUpgrade>();
                            foreach (ProgressionUpgrade upgrade in arcaneUpgrades)
                            {
                                if (upgrade.id == "RegenRate")
                                {
                                    newTower.GetComponent<ArcaneTower>().cooldownEffect(upgrade.returnUpgradeStat());
                                }
                                if (upgrade.id == "StartingSpells")
                                {
                                    newTower.GetComponent<ArcaneTower>().freeEffect((int)upgrade.returnUpgradeStat());
                                }
                            }
                        }

                        // Mark the cell as occupied
                        occupiedCells.Add(cellPos);

                        // If you only want to place one tower per "mode," exit placing mode
                        Destroy(currentPreview);
                        currentPreview = null;
                        isPlacingTower = false;
                        UI.Castbar.Hide();
                    }
                    else
                    {
                        Debug.Log("Invalid placement: either not grass or cell occupied.");
                    }
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
            // If we are NOT placing a tower, make sure no preview object remains
            if (currentPreview != null)
            {
                Destroy(currentPreview);
                currentPreview = null;
            }
        }
    }

    // Call this from a button to toggle placement mode on/off
    public void Place(int tower)
    {
        isPlacingTower = true;
        towerId = tower;
    }

    public void Stop() {
        if (!isPlacingTower) return;

        isPlacingTower = false;
        UI.Castbar.Hide();

        // If turning off, destroy any existing preview
        if (!isPlacingTower && currentPreview != null)
        {
            Destroy(currentPreview);
            currentPreview = null;
        }
    }

    public void updateBoltStatus()
    {
        if (masterMoneyManager.GetMoney() < 15)
        {
            boltIcon.GetComponent<Button>().interactable = false;
            boltText.color = Color.red;
        }
        else
        {
            boltIcon.GetComponent<Button>().interactable = true;
            boltText.color = Color.white;
        }
    }

    public void updateBombStatus()
    {
        if (masterMoneyManager.GetMoney() < 20)
        {
            bombIcon.GetComponent<Button>().interactable = false;
            bombText.color = Color.red;
        }
        else
        {
            bombIcon.GetComponent<Button>().interactable = true;
            bombText.color = Color.white;
        }
    }

    public void updateSniperStatus()
    {
        if (masterMoneyManager.GetMoney() < 20)
        {
            sniperIcon.GetComponent<Button>().interactable = false;
            sniperText.color = Color.red;
        }
        else
        {
            sniperIcon.GetComponent<Button>().interactable = true;
            sniperText.color = Color.white;
        }
    }

    public void updateBubbleStatus()
    {
        if (masterMoneyManager.GetMoney() < 25)
        {
            bubbleIcon.GetComponent<Button>().interactable = false;
            bubbleText.color = Color.red;
        }
        else
        {
            bubbleIcon.GetComponent<Button>().interactable = true;
            bubbleText.color = Color.white;
        }
    }

    public void updateGateStatus()
    {
        if (masterMoneyManager.GetMoney() < 50)
        {
            gateIcon.GetComponent<Button>().interactable = false;
            gateText.color = Color.red;
        }
        else
        {
            gateIcon.GetComponent<Button>().interactable = true;
            gateText.color = Color.white;
        }
    }

    public void updateGatherStatus()
    {
        if (masterMoneyManager.GetMoney() < 50)
        {
            gathererIcon.GetComponent<Button>().interactable = false;
            gathererText.color = Color.red;
        }
        else
        {
            gathererIcon.GetComponent<Button>().interactable = true;
            gathererText.color = Color.white;
        }
    }

    public void updateArcaneStatus()
    {
        if (masterMoneyManager.GetMoney() < 50)
        {
            arcaneIcon.GetComponent<Button>().interactable = false;
            arcaneText.color = Color.red;
        }
        else
        {
            arcaneIcon.GetComponent<Button>().interactable = true;
            arcaneText.color = Color.white;
        }
    }
}