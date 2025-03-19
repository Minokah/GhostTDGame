using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    Game Game;
    UI UI;

    public GameObject towerContainer, spellContainer;

    public Dictionary<string, GameObject> towerList = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> spellList = new Dictionary<string, GameObject>();

    void Awake()
    {
        Game = Game.Get();
        UI = UI.Get();

        ReadTowers();
        ReadSpells();
    }

    public void ReadTowers()
    {
        foreach (Transform child in towerContainer.transform)
        {
            ProgressionEntry objBase = child.GetComponent<ProgressionEntry>();
            if (objBase == null)
            {
                Debug.Log($"[ProgressionManager] A tower {child.name} is missing a ProgressionEntry!");
                continue;
            }

            towerList.Add(objBase.id, child.gameObject);
        }
    }

    public void ReadSpells()
    {
        foreach (Transform child in spellContainer.transform)
        {
            ProgressionEntry objBase = child.GetComponent<ProgressionEntry>();
            if (objBase == null)
            {
                Debug.Log($"[ProgressionManager] A spell {child.name} is missing a ProgressionEntry!");
                continue;
            }

            spellList.Add(objBase.id, child.gameObject);
        }
    }

    public bool IsValidEntry(string type, string id)
    {
        if (type == "towers") return towerList.ContainsKey(id);
        else if (type == "spells") return spellList.ContainsKey(id);
        else return false;
    }

    public int GetPlayerLevel()
    {
        if (!Game.ProfileManager.IsProfileActive()) return 0;
        return Game.StatisticsManager.statistics["levelCount"];
    }

    // Resets progression state for loading
    public void ResetState()
    {
        foreach (GameObject item in towerList.Values)
        {
            item.GetComponent<ProgressionEntry>().ResetState();
        }

        foreach (GameObject item in spellList.Values)
        {
            item.GetComponent<ProgressionEntry>().ResetState();
        }
    }
}
