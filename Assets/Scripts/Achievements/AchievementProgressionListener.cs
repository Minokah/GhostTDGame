using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementProgressionListener : MonoBehaviour
{
    public GameObject entity;
    public AchievementEntry achievement;
    private Game Game;

    private ProgressionUpgrade[] upgrades;
    
    void Start()
    {
        Game = Game.Get();
        upgrades = entity.GetComponents<ProgressionUpgrade>();
    }
    
    void Update()
    {
        if (upgrades[0].level == 3 && upgrades[1].level == 3)
        {
            // Unlock cosmetic and give achievement
            entity.GetComponents<ProgressionCosmetic>()[1].unlocked = true;
            Game.AchievementManager.GrantAchievement(achievement);
            gameObject.SetActive(false);
        }
    }
}
