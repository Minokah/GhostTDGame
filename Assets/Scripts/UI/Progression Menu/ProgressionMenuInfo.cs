using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionMenuInfo : MonoBehaviour
{
    Game Game;
    UI UI;
    CanvasVisible canvas;

    // Header
    public TMP_Text title, description;
    public RawImage icon, banner;

    public Button back, upgradesButton, cosmeticsButton;
    public ProgressionMenuUpgrades upgrades; // upgrades sub menu
    public ProgressionMenuCosmetics cosmetics; // cosmetics sub menu
    public CanvasVisible menuCanvas; // "home" select menu

    bool inSubMenu = false;
    private GameObject currentEntry = null;
    private GameObject previewEntry = null;

    void Start()
    {
        Game = Game.Get();
        UI = UI.Get();
        canvas = GetComponent<CanvasVisible>();
        back.onClick.AddListener(Back);
        upgradesButton.onClick.AddListener(ShowUpgrades);
        cosmeticsButton.onClick.AddListener(ShowCosmetics);
    }

    // Return to the listing menu if on tower/spell page, or back to info page if in a sub menu
    private void Back()
    {
        // In sub menu? Go back to lore screen
        if (inSubMenu)
        {
            inSubMenu = false;
            menuCanvas.Show();
            upgrades.Hide();
            cosmetics.Hide();
        }
        // Go back to the progression main page
        else
        {
            UI.ProgressionMenu.Show();
            canvas.Hide();
        }
    }

    private void ShowUpgrades()
    {
        inSubMenu = true;
        menuCanvas.Hide();
        upgrades.Show(currentEntry);
    }

    private void ShowCosmetics()
    {
        inSubMenu = true;
        menuCanvas.Hide();
        cosmetics.Show(currentEntry, previewEntry);
    }

    public void Show(GameObject entry, GameObject preview)
    {
        currentEntry = entry;
        previewEntry = preview;

        ProgressionEntry prog = entry.GetComponent<ProgressionEntry>();

        // If this is a spell, hide the cosmetic menu. Spells don't have decorations
        if (prog.type == "towers") cosmeticsButton.gameObject.SetActive(true);
        else if (prog.type == "spells") cosmeticsButton.gameObject.SetActive(false);

        canvas.Show();
        menuCanvas.Show();
        upgrades.Hide();
        cosmetics.Hide();

        // set the stuff
        title.text = prog.name;
        icon.texture = Resources.Load<Texture>($"Icons/{prog.type}/{prog.id}");
        description.text = prog.extendedDescription;
    }

    public void Hide()
    {
        canvas.Hide();
    }
}
