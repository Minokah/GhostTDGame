using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatisticsManager : MonoBehaviour
{
    public Dictionary<string, int> statistics;
    public Dictionary<string, int> session;
    public int level;
    public TMP_Text displayedMoney;

    void Start()
    {
        ResetState();
    }

    // Reset statistics to the default state for profile loads
    public void ResetState()
    {
        statistics = new Dictionary<string, int>()
        {
            { "level", 999 },
            { "exp", 0 },
            { "unlockTokens", 0 },
            { "money", 999999 }
        };

        session = new Dictionary<string, int>()
        {
            { "money", 35 }
        };
    }

    public void addMoney(int moneyAmount)
    {
        session["money"] = session["money"] + moneyAmount;
        displayedMoney.text = GetMoney().ToString();
    }

    public int GetMoney()
    {
        return session["money"];
    }
}
