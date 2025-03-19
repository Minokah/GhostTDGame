using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenuEntry : MonoBehaviour
{
    public enum STATUS { BUYABLE, NOT_BUYABLE, DISABLED };
    public GameObject entity;
    UI UI;
    Game Game;
    Button button;
    public Image costPanel;
    public TMP_Text costText;
    public GameObject locked;
    public int towerId; // Why are we using IDs? We have an entity linked to it above

    public bool unlocked = false;

    void Start()
    {
        UI = UI.Get();
        Game = Game.Get();
        button = GetComponent<Button>();
        button.onClick.AddListener(Click);
    }

    public void SetUnlocked(bool unlocked)
    {
        this.unlocked = unlocked;
        button.interactable = unlocked;
        locked.SetActive(!unlocked);
    }

    public void SetBuyable(STATUS status)
    {
        if (!unlocked) return;

        switch (status)
        {
            case STATUS.BUYABLE:
                button.interactable = true;
                costPanel.color = new Color(76f / 255f, 209f / 255f, 55f / 255f, 1f);
                break;
            case STATUS.NOT_BUYABLE:
                button.interactable = false;
                costPanel.color = new Color(209f / 255f, 58f / 255f, 55f / 255f, 1f);
                break;
            case STATUS.DISABLED:
                button.interactable = false;
                costPanel.color = new Color(30f / 255f, 30f / 255f, 30f / 255f, 1f);
                costText.text = "Purchased";
                break;
        }
    }

    private void Click() {
        Game.TowerPlacementManager.Place(towerId);
        UI.Castbar.Set(entity, CastbarUI.Type.Build);
        UI.Castbar.Show();
    }
}
