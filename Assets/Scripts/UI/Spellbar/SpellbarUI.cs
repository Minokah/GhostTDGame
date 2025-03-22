using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class SpellbarUI : MonoBehaviour
{
    public Button spell1Button, spell2Button, spell3Button;
    public RectTransform spell1Cooldown, spell2Cooldown, spell3Cooldown;
    public GameObject eurekaPanel;
    public TMP_Text eurekaAmt;
    UI UI;
    Game Game;

    CanvasVisible canvas;

    void Start()
    {
        UI = UI.Get();
        Game = Game.Get();
        spell1Button.onClick.AddListener(Spell1Click);
        spell2Button.onClick.AddListener(Spell2Click);
        spell3Button.onClick.AddListener(Spell3Click);
        canvas = GetComponent<CanvasVisible>();
    }

    // Update is called once per frame
    void Update()
    {
        int selected = 0;
        // Pressing 1 2 or 3 will select the corresponding spell on the castbar, if spell is not being cooleddown
        if (Input.GetKey(KeyCode.Alpha1) && spell1Button.interactable) selected = 1;
        if (Input.GetKey(KeyCode.Alpha2) && spell2Button.interactable) selected = 2;
        if (Input.GetKey(KeyCode.Alpha3) && spell3Button.interactable) selected = 3;

        switch (selected) {
            case 1:
                Spell1Click();
                break;
            case 2:
                Spell2Click();
                break;
            case 3:
                Spell3Click();
                break;
        }
    }

    // Or clicking on them will also pull up the castbar
    private void Spell1Click()
    {
        if (Game.SpellManager.spell1 == null) return;
        UI.Castbar.Set(Game.SpellManager.spell1, CastbarUI.Type.Cast);
        UI.Castbar.Show();
        Game.SpellManager.Place(0);
    }

    private void Spell2Click()
    {
        if (Game.SpellManager.spell2 == null) return;
        UI.Castbar.Set(Game.SpellManager.spell2, CastbarUI.Type.Cast);
        UI.Castbar.Show();
        Game.SpellManager.Place(1);
    }

    private void Spell3Click()
    {
        if (Game.SpellManager.spell3 == null) return;
        UI.Castbar.Set(Game.SpellManager.spell3, CastbarUI.Type.Cast);
        UI.Castbar.Show();
        Game.SpellManager.Place(2);
    }

    public void Show()
    {
        canvas.Show();
    }

    public void Hide()
    {
        canvas.Hide(0.05f);
    }

    // Refresh the icons on the HUD
    public void Refresh()
    {
        if (Game.SpellManager.spell1 != null)
        {
            spell1Button.GetComponent<SpellButton>().Set(Game.SpellManager.spell1.GetComponent<ProgressionEntry>().id);
            spell1Button.GetComponent<StatsHoverEntity>().entity = Game.SpellManager.spell1;
        }
        else
        {
            spell1Button.GetComponent<SpellButton>().Set(Game.SpellManager.spell1.GetComponent<ProgressionEntry>().id);
            spell1Button.GetComponent<StatsHoverEntity>().entity = null;
        }

        if (Game.SpellManager.spell2 != null)
        {
            spell2Button.GetComponent<SpellButton>().Set(Game.SpellManager.spell2.GetComponent<ProgressionEntry>().id);
            spell2Button.GetComponent<StatsHoverEntity>().entity = Game.SpellManager.spell2;
        }
        else
        {
            spell2Button.GetComponent<SpellButton>().Set("None");
            spell2Button.GetComponent<StatsHoverEntity>().entity = null;
        }

        if (Game.SpellManager.spell3 != null)
        {
            spell3Button.GetComponent<SpellButton>().Set(Game.SpellManager.spell3.GetComponent<ProgressionEntry>().id);
            spell3Button.GetComponent<StatsHoverEntity>().entity = Game.SpellManager.spell3;
        }
        else
        {
            spell3Button.GetComponent<SpellButton>().Set("None");
            spell3Button.GetComponent<StatsHoverEntity>().entity = null;
        }
    }

    // Eureka stacks
    public void SetEureka(int amt)
    {
        // Negative = hide
        if (amt < 0) eurekaPanel.SetActive(false);
        else
        {
            eurekaPanel.SetActive(true);
            eurekaAmt.text = "Eurekas: " + amt;
        }
    }

    public void SetSpellCooldown(int slot, float amount)
    {
        // map image to panel
        RectTransform cd = null;
        if (slot == 1) cd = spell1Cooldown;
        if (slot == 2) cd = spell2Cooldown;
        if (slot == 3) cd = spell3Cooldown;

        // Nothing? Don't do anything
        if (cd == null) return;

        if (amount >= 1) cd.gameObject.SetActive(false);
        else
        {
            cd.gameObject.SetActive(true);
            cd.sizeDelta = new Vector2((1 - amount) * 90, 90);
        }
    }

    public void SetSpellState(int slot, bool enabled)
    {
        if (slot == 1) spell1Button.interactable = enabled;
        if (slot == 2) spell2Button.interactable = enabled;
        if (slot == 3) spell3Button.interactable = enabled;
    }
	
	public void ResetSpellCooldown()
	{
		RectTransform cd = null;
		cd = spell1Cooldown;
		cd.gameObject.SetActive(false);
		cd = spell2Cooldown;
		cd.gameObject.SetActive(false);
		cd = spell3Cooldown;
		cd.gameObject.SetActive(false);
	}
}
