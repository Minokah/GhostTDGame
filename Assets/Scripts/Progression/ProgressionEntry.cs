using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionEntry : MonoBehaviour
{
    public string id;
    public string type;
    public string name;
    public string description;
    public string extendedDescription;
    public int requireLevel;

    public void ResetState()
    {
        foreach (ProgressionCosmetic item in GetComponents<ProgressionCosmetic>())
        {
            item.unlocked = false;
            item.equipped = false;
        }

        foreach (ProgressionUpgrade item in GetComponents<ProgressionUpgrade>())
        {
            item.level = 0;
        }
    }
}
