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
            
            // Kills
            { "totalKills", 0 },
            { "basicKills", 0 },
            { "toughKills", 0 },
            { "fastKills", 0 },
            { "machoKills", 0 },
            { "balloonKills", 0 },
            { "mixtapeKills", 0 },
            { "cameraKills", 0 },
            
            // Towers
            { "towersPlaced", 0 },
            { "boltPlaced", 0 },
            { "cannonPlaced", 0 },
            { "sniperPlaced", 0 },
            { "bubblePlaced", 0 },
            { "gatePlaced", 0 },
            { "gathererPlaced", 0 },
            { "arcanePlaced", 0 },
            
            // Spells
            { "spellsCasted", 0 },
            { "fireCasts", 0 },
            { "iceCasts", 0 },
            { "lightningCasts", 0 },
            { "windCasts", 0 },
            { "timeCasts", 0 },
        };

        session = new Dictionary<string, int>()
        {
            { "money", 45 },
            
            // Kills
            { "totalKills", 0 },
            { "basicKills", 0 },
            { "toughKills", 0 },
            { "fastKills", 0 },
            { "machoKills", 0 },
            { "balloonKills", 0 },
            { "mixtapeKills", 0 },
            { "cameraKills", 0 },
            
            // Towers
            { "towersPlaced", 0 },
            { "boltPlaced", 0 },
            { "cannonPlaced", 0 },
            { "sniperPlaced", 0 },
            { "bubblePlaced", 0 },
            { "gatePlaced", 0 },
            { "gathererPlaced", 0 },
            { "arcanePlaced", 0 },
            
            // Spells
            { "spellsCasted", 0 },
            { "fireCasts", 0 },
            { "iceCasts", 0 },
            { "lightningCasts", 0 },
            { "windCasts", 0 },
            { "timeCasts", 0 },
        };
		displayedMoney.text = GetMoney().ToString();
    }
	
	public void ResetGameState()
    {
        session = new Dictionary<string, int>()
        {
            { "money", 45 },
            
            // Kills
            { "totalKills", 0 },
            { "basicKills", 0 },
            { "toughKills", 0 },
            { "fastKills", 0 },
            { "machoKills", 0 },
            { "balloonKills", 0 },
            { "mixtapeKills", 0 },
            { "cameraKills", 0 },
            
            // Towers
            { "towersPlaced", 0 },
            { "boltPlaced", 0 },
            { "cannonPlaced", 0 },
            { "sniperPlaced", 0 },
            { "bubblePlaced", 0 },
            { "gatePlaced", 0 },
            { "gathererPlaced", 0 },
            { "arcanePlaced", 0 },
            
            // Spells
            { "spellsCasted", 0 },
            { "fireCasts", 0 },
            { "iceCasts", 0 },
            { "lightningCasts", 0 },
            { "windCasts", 0 },
            { "timeCasts", 0 },
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

    public void IncrementStatistics(string name, int amount = 1)
    {
        if (statistics.ContainsKey(name)) statistics[name] += amount;
        if (session.ContainsKey(name)) session[name] += amount;
    }
}
