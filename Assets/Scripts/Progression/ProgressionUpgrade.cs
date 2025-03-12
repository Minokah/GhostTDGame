using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionUpgrade : MonoBehaviour
{
    public string id, name;
    public int level = 0;
    public float amount1, amount2, amount3;

    public float returnUpgradeStat()
    {
        if (level == 1)
        {
            return amount1;
        }
        else if (level == 2)
        {
            return amount2;
        }
        else if (level == 3)
        {
            return amount3;
        }
        else
        {
            return 0f;
        }
    }
}
