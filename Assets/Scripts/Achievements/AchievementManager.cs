using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    Game Game;
    UI UI;
    public Dictionary<string, AchievementEntry> list = new Dictionary<string, AchievementEntry>();
    public GameObject listeners;

    void Awake()
    {
        Game = Game.Get();
        UI = UI.Get();
        
        // Recursively parse achievements
        Read(transform);
        
    }

    // Recursively parse and find achievements
    public void Read(Transform obj)
    {
        foreach (Transform child in obj.transform) Read(child);
        AchievementEntry objBase = obj.GetComponent<AchievementEntry>();
        if (objBase != null) list.Add(objBase.id, objBase);
    }

    // Grant the achievement through normal means or steathily (like when loading)
    public bool GrantAchievement(string id)
    {
        // Achievement doesn't exist? Ignore
        if (!list.ContainsKey(id)) return false;
        return GrantAchievement(list[id]);
    }

    public bool GrantAchievement(AchievementEntry entry)
    {
        // User already has achievement? ignore
        if (entry.unlocked) return false;

        // Display ui
        UI.NotificationUI.Display(new NotificationEntry(entry.name, entry.description, entry.requireType, entry.requireID));

        // Add to achievements list
        entry.unlocked = true;
        Game.ProfileManager.Save();

        return true;
    }

    public int GetProgress(string type, string id)
    {
        switch (type)
        {
            case "achievements":
                return list[id].unlocked == true ? 1 : 0;
            case "statistics":
                if (!Game.StatisticsManager.statistics.ContainsKey(id)) return 0;
                return Game.StatisticsManager.statistics[id];
            case "session-statistics":
                if (!Game.StatisticsManager.session.ContainsKey(id)) return 0;
                return Game.StatisticsManager.session[id];
        }

        return 0;
    }

    // Is the achievement unlocked?
    public bool IsUnlocked(string id)
    {
        return list[id].unlocked;
    }

    // Resets progression state for loading
    public void ResetState()
    {
        foreach (AchievementEntry item in list.Values)
        {
            item.unlocked = false;
        }

        foreach (Transform item in listeners.transform)
        {
            item.gameObject.SetActive(true);
        }
    }
}
