using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class SpellManager : MonoBehaviour
{
    public bool isUsingSpell = false;

    public GameObject spell1, spell2, spell3;
    public GameObject spell1Preview, spell2Preview, spell3Preview;

    GameObject spellPrefab;

    GameObject spellPreviewPrefab;

    public Tilemap grassTilemap;

    public LayerMask groundLayer;

    // The currently visible spell preview
    private GameObject currentPreview;

    // Used to diffrentiate the spell
    private int spellSlotId;

    UI UI;

    void Start()
    {
        UI = UI.Get();
    }

    void Update()
    {
        // Only do placement logic if we are currently in "placing" mode
        if (isUsingSpell)
        {
            // detect which spell slot was clicked
            if (spellSlotId == 0)
            {
                spellPrefab = spell1;
                spellPreviewPrefab = spell1Preview;
            }
            else if (spellSlotId == 1)
            {
                spellPrefab = spell2;
                spellPreviewPrefab = spell2Preview;
            }
            else
            {
                spellPrefab = spell3;
                spellPreviewPrefab = spell3Preview;
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
}