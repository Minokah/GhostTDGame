using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionMenuUpgradeEntry : MonoBehaviour
{
    public ProgressionMenuUpgradeLevel level1, level2, level3;
    public TMP_Text name, tokenAmount;
    public Button upgrade;

    public void SetLevel(int level = 0)
    {
        level1.SetEquipped(false);
        level2.SetEquipped(false);
        level3.SetEquipped(false);

        if (level > 0) level1.SetEquipped(true);
        if (level > 1) level2.SetEquipped(true);
        if (level > 2) level3.SetEquipped(true);
    }

    public void SetMaxState(bool max)
    {
        if (max)
        {
            upgrade.interactable = false;
            tokenAmount.text = "--";
        }
        else
        {
            upgrade.interactable = true;
            tokenAmount.text = "1";
        }
    }
}
