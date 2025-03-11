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
                string text = "Select a spell on the right to switch to.";
                hint.text = $"{text}\n{changeEntity.name} >>>> ?";
            }
        }
        else hint.text = "Click on a slot above and then a spell on the listing to add or replace it.";
    }

    // Or clicking on them will also pull up the castbar
    private void Spell1Click()
    {
        changeEntity = Game.SpellManager.spell1.GetComponent<ProgressionEntry>();
        changeSlot = 1;
        changeState = true;
        UI.ProgressionMenu.listing.UpdateSpellChangeText();
    }

    private void Spell2Click()
    {
        changeEntity = Game.SpellManager.spell2.GetComponent<ProgressionEntry>();
        changeSlot = 2;
        changeState = true;
        UI.ProgressionMenu.listing.UpdateSpellChangeText();
    }

    private void Spell3Click()
    {
        changeEntity = Game.SpellManager.spell3.GetComponent<ProgressionEntry>();
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
}
