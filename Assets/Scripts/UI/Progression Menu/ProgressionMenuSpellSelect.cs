using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionMenuSpellSelect : MonoBehaviour
{
    public Button spell1Button, spell2Button, spell3Button;
    public TMP_Text hint;
    UI UI;
    Game Game;

    // Changing?
    public bool changeState = false;
    int changeSlot = 0;
    ProgressionEntry changeEntity = null;


    void Start()
    {
        UI = UI.Get();
        Game = Game.Get();
        spell1Button.onClick.AddListener(Spell1Click);
        spell2Button.onClick.AddListener(Spell2Click);
        spell3Button.onClick.AddListener(Spell3Click);
    }

    void Update()
    {
        if (changeState)
        {
            // Cancelling?
            if (Input.GetMouseButtonUp(1)) changeState = false;
            else
            {
                string name = changeEntity != null ? changeEntity.name : "None";
                string text = "Select a spell on the right to switch to.";
                hint.text = $"{text}\n{name} >>>> ?\n\n[ RClick ] Cancel";
            }
        }
        else hint.text = "Click on a slot above and then a spell on the listing to add or replace it.";
    }

    // Or clicking on them will also pull up the castbar
    private void Spell1Click()
    {
        if (Game.SpellManager.spell1 != null) changeEntity = Game.SpellManager.spell1.GetComponent<ProgressionEntry>();
        else changeEntity = null;
        changeSlot = 1;
        changeState = true;
        UI.ProgressionMenu.listing.UpdateSpellChangeText();
    }

    private void Spell2Click()
    {
        if (Game.SpellManager.spell2 != null) changeEntity = Game.SpellManager.spell2.GetComponent<ProgressionEntry>();
        else changeEntity = null;
        changeSlot = 2;
        changeState = true;
        UI.ProgressionMenu.listing.UpdateSpellChangeText();
    }

    private void Spell3Click()
    {
        if (Game.SpellManager.spell3 != null) changeEntity = Game.SpellManager.spell3.GetComponent<ProgressionEntry>();
        else changeEntity = null;
            changeSlot = 3;
        changeState = true;
        UI.ProgressionMenu.listing.UpdateSpellChangeText();
    }

    public void ChangeSpell(GameObject spell)
    {
        ProgressionEntry entry = spell.GetComponent<ProgressionEntry>();
        if (changeSlot == 1)
        {
            Game.SpellManager.spell1 = spell;
            spell1Button.GetComponent<StatsHoverEntity>().entity = spell;
            spell1Button.GetComponent<SpellButton>().Set(entry.id);
        }
        else if (changeSlot == 2)
        {
            Game.SpellManager.spell2 = spell;
            spell2Button.GetComponent<StatsHoverEntity>().entity = spell;
            spell2Button.GetComponent<SpellButton>().Set(entry.id);
        }
        else if (changeSlot == 3)
        {
            Game.SpellManager.spell3 = spell;
            spell3Button.GetComponent<StatsHoverEntity>().entity = spell;
            spell3Button.GetComponent<SpellButton>().Set(entry.id);
        }

        StopChanging();
    }

    public void StopChanging()
    {
        changeState = false;
        changeSlot = 0;
        changeEntity = null;
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
}
