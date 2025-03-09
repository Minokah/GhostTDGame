using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Profiling;
using static UnityEngine.EventSystems.EventTrigger;

public class ProfileManager : MonoBehaviour
{
    Game Game;
    public string playerName = "Player";
    public bool loaded = false;
    // Start is called before the first frame update
    void Start()
    {
        Game = Game.Get();
        Load();
    }

    // Load a profile from My Documents/My Games/GhostTDGame
    public void Load()
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\My Games\\GhostTDGame";
        // Create "My Games" folder if it doesn't exist (standard folder for.. a lot of games)
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        // If the profile.json doesn't exist, make a new one and save it
        if (!File.Exists(path + "\\profile.json"))
        {
            Save();
            return;
        }

        // Otherwise, read from profile.json (temporary)
        StreamReader reader = new StreamReader(path + "\\profile.json");
        string content = "";
        string line = reader.ReadLine();
        try
        {
            while (line != null)
            {
                content += line;
                line = reader.ReadLine();
            }
        }
        catch (Exception e)
        {
            Debug.Log("[ProfileManager] Failed to read from profile.json! " + e);
        }
        reader.Close();

        try
        {
            // Create blank profile to read to
            var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
            ParseLoad(json);
            
        }
        catch (Exception e)
        {
            Debug.Log("[ProfileManager] Failed to parse from profile.json! " + e);
        }
    }

    private void ParseLoad(Dictionary<string, object> json)
    {
        Game.StatisticsManager.ResetState();
        playerName = "Player";
        playerName = (string)json["name"];

        // Parse progressions

        // Reset states...
        Game.AchievementManager.ResetState();
        Game.ProgressionManager.ResetState();

        // Starting with achievements
        var achievements = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json["achievements"].ToString());
        foreach (Dictionary<string, object> item in achievements)
        {
            if ((string)item["unlocked"] == "1") Game.AchievementManager.list[(string)item["id"]].unlocked = true;
        }

        // Towers
        var towers = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json["towers"].ToString());
        LoadProgressionEntries("towers", towers);

        // Spells
        var spells = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json["spells"].ToString());
        LoadProgressionEntries("spells", spells);

        // Parse statistics
        var statistics = JsonConvert.DeserializeObject<Dictionary<string, string>>(json["statistics"].ToString());

        // We want them in int form.. the values are not hardcoded to allow for any values if one chooses (for modding)
        Dictionary<string, int> newStatistics = new Dictionary<string, int>();
        foreach (string stat in statistics.Keys)
        {
            newStatistics.Add(stat, int.Parse(statistics[stat]));
        }
        Game.StatisticsManager.statistics = newStatistics;

        loaded = true;
    }

    private void LoadProgressionEntries(string type, List<Dictionary<string, object>> container)
    {
        foreach (Dictionary<string, object> item in container)
        {
            // Not a valid tower (for whatever reason)? Ignore
            if (!Game.ProgressionManager.IsValidEntry(type, (string)item["id"])) continue;
            Dictionary<string, GameObject> list = null;
            if (type == "towers") list = Game.ProgressionManager.towerList;
            else if (type == "spells") list = Game.ProgressionManager.spellList;

            GameObject entry = list[(string)item["id"]];

            // Parse Upgrades and Cosmetics
            var upgrades = JsonConvert.DeserializeObject<Dictionary<string, string>>(item["upgrades"].ToString());
            var upgrComps = entry.GetComponents<ProgressionUpgrade>();
            for (int i = 1; i != 3; i++) upgrComps[i - 1].level = int.Parse(upgrades[i.ToString()]);

            // Spells don't have cosmetics
            if (type == "spells") continue;
            var cosmetics = JsonConvert.DeserializeObject<Dictionary<string, string>>(item["cosmetics"].ToString());
            var cosmComps = entry.GetComponents<ProgressionCosmetic>();
            for (int i = 1; i != 3; i++) cosmComps[i - 1].unlocked = cosmetics[i.ToString()] == "1";
        }
    }

    // Save current profile to a save file
    public void Save()
    {
        // Prepare save data
        Dictionary<string, object> save = new Dictionary<string, object>();
        List<object> achievements = new List<object>();
        List<object> towers = new List<object>();
        List<object> spells = new List<object>();

        save.Add("name", playerName);

        foreach (AchievementEntry entry in Game.AchievementManager.list.Values)
        {
            Dictionary<string, object> saveItem = new Dictionary<string, object>();
            saveItem.Add("id", entry.id);
            saveItem.Add("unlocked", entry.unlocked ? "1" : "0");
            achievements.Add(saveItem);
        }
        save.Add("achievements", achievements);

        foreach (GameObject entry in Game.ProgressionManager.towerList.Values)
        {
            ProgressionEntry prog = entry.GetComponent<ProgressionEntry>();
            Dictionary<string, object> saveItem = new Dictionary<string, object>();
            saveItem.Add("id", prog.id);

            // Upgrades and Cosmetics are in format: "1": "0" for entry 1, not unlocked
            var upgrades = entry.GetComponents<ProgressionUpgrade>();
            Dictionary<string, object> saveUpgr = new Dictionary<string, object>();
            for (int i = 1; i != 3; i++) saveUpgr.Add(i.ToString(), upgrades[i - 1].level.ToString());
            saveItem.Add("upgrades", saveUpgr);

            var cosmetics = entry.GetComponents<ProgressionCosmetic>();
            Dictionary<string, object> cosmUpgr = new Dictionary<string, object>();
            for (int i = 1; i != 3; i++) cosmUpgr.Add(i.ToString(), cosmetics[i - 1].unlocked == true ? "1" : "0");
            saveItem.Add("cosmetics", cosmUpgr);

            towers.Add(saveItem);
        }
        save.Add("towers", towers);

        foreach (GameObject entry in Game.ProgressionManager.spellList.Values)
        {
            ProgressionEntry prog = entry.GetComponent<ProgressionEntry>();
            Dictionary<string, object> saveItem = new Dictionary<string, object>();
            saveItem.Add("id", prog.id);

            // Upgrades and Cosmetics are in format: "1": "0" for entry 1, not unlocked
            var upgrades = entry.GetComponents<ProgressionUpgrade>();
            Dictionary<string, object> saveUpgr = new Dictionary<string, object>();
            for (int i = 1; i != 3; i++) saveUpgr.Add(i.ToString(), upgrades[i - 1].level.ToString());
            saveItem.Add("upgrades", saveUpgr);

            spells.Add(saveItem);
        }
        save.Add("spells", spells);

        // Statistics are stored as strings, but they are initially integers, convert them
        Dictionary<string, string> convertedStats = new Dictionary<string, string>();
        
        foreach (string stat in Game.StatisticsManager.statistics.Keys)
        {
            convertedStats.Add(stat, Game.StatisticsManager.statistics[stat].ToString());
        }
        save.Add("statistics", convertedStats);

        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\My Games\\GhostTDGame";

        // Create "My Games" folder if it doesn't exist (standard folder for.. a lot of games)
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        StreamWriter writer = new StreamWriter(path + "\\profile.json");
        writer.Write(JsonConvert.SerializeObject(save));
        writer.Close();

        loaded = true;
    }

    public bool IsProfileActive(string s = "")
    {
        if (!loaded)
        {
            if (s != "") Debug.Log($"[ProfileManager] -> [{s}] There is no active profile!");
            return false;
        }
        return true;
    }
}
