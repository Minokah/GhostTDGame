using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;
using static UnityEngine.EventSystems.EventTrigger;

public class ProgressionMenuUpgrades : MonoBehaviour
{
    Game Game;
    UI UI;
    CanvasVisible canvas;
    public ProgressionMenuUpgradeEntry upgrade1, upgrade2;
    public ProgressionMenuSpecialty specialty1, specialty2;
    public GameObject specialtyBlocker;
    public TMP_Text specialtyDescription;
    private ProgressionUpgrade upgrade1Obj, upgrade2Obj;
    private ProgressionSpecialUpgrade special1Obj, special2Obj;

    void Start()
    {
        Game = Game.Get();
        UI = UI.Get();
        canvas = GetComponent<CanvasVisible>();
        upgrade1.upgrade.onClick.AddListener(UpgradeFirst);
        upgrade2.upgrade.onClick.AddListener(UpgradeSecond);
        specialty1.button.onClick.AddListener(SetSpecialtyOne);
        specialty2.button.onClick.AddListener(SetSpecialtyTwo);
    }

    public void Show(GameObject entry)
    {
        canvas.Show();
        upgrade1Obj = null;
        upgrade2Obj = null;
        special1Obj = null;
        special2Obj = null;

        var items = entry.GetComponents<ProgressionUpgrade>();
        int i = 0;
        foreach (ProgressionUpgrade upgr in items)
        {
            if (i == 0) upgrade1Obj = upgr;
            else if (i == 1) upgrade2Obj = upgr;
            i++;
        }

        var spItems = entry.GetComponents<ProgressionSpecialUpgrade>();
        i = 0;
        foreach (ProgressionSpecialUpgrade upgr in spItems)
        {
            if (i == 0) special1Obj = upgr;
            else if (i == 1) special2Obj = upgr;
            i++;
        }

        Refresh();
    }

    public void Hide()
    {
        canvas.Hide();
    }

    public void Refresh()
    {
        UI.ProgressionMenu.currency.Refresh();
        specialtyBlocker.SetActive(true);
        upgrade1.gameObject.SetActive(false);
        upgrade2.gameObject.SetActive(false);
        specialty1.SetEquipped(false);
        specialty2.SetEquipped(false);
        specialtyDescription.text = "This box is for the selected Specialty.";

        bool firstMax = false;
        bool secondMax = false;

        // Upgrade 1
        if (upgrade1Obj != null)
        {
            upgrade1.gameObject.SetActive(true);
            upgrade1.name.text = upgrade1Obj.name;
            upgrade1.level1.amount.text = upgrade1Obj.amount1.ToString();
            upgrade1.level2.amount.text = upgrade1Obj.amount2.ToString();
            upgrade1.level3.amount.text = upgrade1Obj.amount3.ToString();
            upgrade1.SetLevel(upgrade1Obj.level);

            firstMax = upgrade1Obj.level >= 3;
            upgrade1.SetMaxState(firstMax);
        }

        // Upgrade 2
        if (upgrade2Obj != null)
        {
            upgrade2.gameObject.SetActive(true);
            upgrade2.name.text = upgrade2Obj.name;
            upgrade2.level1.amount.text = upgrade2Obj.amount1.ToString();
            upgrade2.level2.amount.text = upgrade2Obj.amount2.ToString();
            upgrade2.level3.amount.text = upgrade2Obj.amount3.ToString();
            upgrade2.SetLevel(upgrade2Obj.level);

            secondMax = upgrade2Obj.level >= 3;
            upgrade2.SetMaxState(secondMax);
        }

        // Only show specialties if fully maxed both upgrade trees
        if (upgrade1Obj != null && !firstMax) return;
        if (upgrade2Obj != null && !secondMax) return;

        specialtyBlocker.SetActive(false);

        // Specialty Upgrade 1
        if (special1Obj != null)
        {
            specialty1.name.text = special1Obj.name;
            specialty1.SetEquipped(special1Obj.level == 1);

            if (special1Obj.level == 1) specialtyDescription.text = special1Obj.description;
        }

        // Specialty Upgrade 2
        if (special2Obj != null)
        {
            specialty2.name.text = special2Obj.name;
            specialty2.SetEquipped(special2Obj.level == 1);

            if (special2Obj.level == 1) specialtyDescription.text = special2Obj.description;
        }
    }
    private void UpgradeFirst()
    {
        if (Game.StatisticsManager.statistics["tokens"] < 1) return;
        if (upgrade1Obj.level == 3) return;

        upgrade1Obj.level++;
        Game.StatisticsManager.removeTokens(1);
        Game.ProfileManager.Save();
        Refresh();
    }

    private void UpgradeSecond()
    {
        if (Game.StatisticsManager.statistics["tokens"] < 1) return;
        if (upgrade2Obj.level == 3) return;

        upgrade2Obj.level++;
        Game.StatisticsManager.removeTokens(1);
        Game.ProfileManager.Save();
        Refresh();
    }

    private void SetSpecialtyOne()
    {
        special1Obj.level = 1;
        special2Obj.level = 0;
        Game.ProfileManager.Save();
        Refresh();
    }

    private void SetSpecialtyTwo()
    {
        special1Obj.level = 0;
        special2Obj.level = 1;
        Game.ProfileManager.Save();
        Refresh();
    }
}
