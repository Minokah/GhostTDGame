using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class ProgressionMenuListing : MonoBehaviour
{
    CanvasVisible canvas, towerCanvas, spellCanvas;
    public RectTransform toggleArea;
    public GameObject towerContainer, spellContainer;
    public Button typeToggleButton;

    bool mode = false; // false = towers, true = spells

    void Start()
    {
        canvas = GetComponent<CanvasVisible>();
        towerCanvas = towerContainer.GetComponent<CanvasVisible>();
        spellCanvas = spellContainer.GetComponent<CanvasVisible>();
        typeToggleButton.onClick.AddListener(SwitchType);
    }

    // Show and refresh the listing menu
    public void Show()
    {
        mode = false;
        canvas.Show();
        ShowTowers();
    }

    public void Hide()
    {
        canvas.Hide();
    }

    // Refresh all towers and spells container
    private void RefreshContainer(GameObject t)
    {
        foreach (Transform item in t.transform)
        {
            ProgressionMenuEntry entry = item.GetComponent<ProgressionMenuEntry>();
            if (entry != null) entry.Refresh();
        }
    }

    public void SwitchType()
    {
        StopSpellChange();
        mode = !mode;
        if (!mode) ShowTowers();
        else ShowSpells();
    }

    // Show list of towers
    private void ShowTowers()
    {
        toggleArea.anchoredPosition = new Vector2(0, 0);
        towerCanvas.Show();
        spellCanvas.Hide();
        RefreshContainer(towerContainer);
    }

    // Show list of spells
    private void ShowSpells()
    {
        toggleArea.anchoredPosition = new Vector2(365, 0);
        towerCanvas.Hide();
        spellCanvas.Show();
        RefreshContainer(spellContainer);
        spellContainer.GetComponent<ProgressionMenuSpellSelect>().Refresh();
    }

    public void StopSpellChange()
    {
        spellContainer.GetComponent<ProgressionMenuSpellSelect>().StopChanging();
    }

    public void ChangeSpell(GameObject entry)
    {
        spellContainer.GetComponent<ProgressionMenuSpellSelect>().ChangeSpell(entry);
    }
    
    public void UpdateSpellChangeText(bool changing)
    {
        foreach (Transform item in spellContainer.transform)
        {
            ProgressionMenuEntry entry = item.GetComponent<ProgressionMenuEntry>();
            if (entry != null) entry.SetChanging(changing);
        }
    }

    public bool IsSpellSelecting()
    {
        return spellContainer.GetComponent<ProgressionMenuSpellSelect>().changeState;
    }
}
