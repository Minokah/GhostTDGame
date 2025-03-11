using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class SpellbarUI : MonoBehaviour
{
    public Button spell1Button, spell2Button, spell3Button;
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
        // Pressing 1 2 or 3 will select the corresponding spell on the castbar
        if (Input.GetKey(KeyCode.Alpha1)) selected = 1;
        if (Input.GetKey(KeyCode.Alpha2)) selected = 2;
        if (Input.GetKey(KeyCode.Alpha3)) selected = 3;

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
        UI.Castbar.Set(Game.SpellManager.spell1, CastbarUI.Type.Cast);
        UI.Castbar.Show();
        Game.SpellManager.Place(0);
    }

    private void Spell2Click()
    {
        UI.Castbar.Set(Game.SpellManager.spell2, CastbarUI.Type.Cast);
        UI.Castbar.Show();
        Game.SpellManager.Place(1);
    }

    private void Spell3Click()
    {
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
        spell1Button.GetComponent<SpellButton>().Set(Game.SpellManager.spell1.GetComponent<ProgressionEntry>().id);
        spell1Button.GetComponent<StatsHoverEntity>().entity = Game.SpellManager.spell1;
        spell2Button.GetComponent<SpellButton>().Set(Game.SpellManager.spell2.GetComponent<ProgressionEntry>().id);
        spell2Button.GetComponent<StatsHoverEntity>().entity = Game.SpellManager.spell2;
        spell3Button.GetComponent<SpellButton>().Set(Game.SpellManager.spell3.GetComponent<ProgressionEntry>().id);
        spell3Button.GetComponent<StatsHoverEntity>().entity = Game.SpellManager.spell3;
    }
}
