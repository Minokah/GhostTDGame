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
            { "tokens", 0 },
            { "levelCount", 0 },
        };

        session = new Dictionary<string, int>()
        {
            { "money", 45 }
        };
		displayedMoney.text = GetMoney().ToString();
    }
	
	public void ResetLevelMoney()
    {
        session = new Dictionary<string, int>()
        {
            { "money", 45 }
        };
		displayedMoney.text = GetMoney().ToString();
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
	
    public void addLevel(int levelAmount)
    {
        statistics["levelCount"] = statistics["levelCount"] + levelAmount;
    }
	
	public int getLevel()
    {
        return (int)statistics["levelCount"];
    }
	
	public void addTokens(int tokenAmount)
    {
        statistics["tokens"] = statistics["tokens"] + tokenAmount;
    }

    public void removeTokens(int tokenAmount)
    {
        if (statistics["tokens"] >= tokenAmount) statistics["tokens"] -= tokenAmount;
    }
}
