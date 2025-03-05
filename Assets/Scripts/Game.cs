using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public StatisticsManager StatisticsManager;
    public ProgressionManager ProgressionManager;
    public AchievementManager AchievementManager;
    public ProfileManager ProfileManager;
    public GameplayCameraController GameplayCameraController;
    public TowerPlacementManager TowerPlacementManager;
    public SpellManager SpellManager;

    public static Game Get() {
        return GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
    }
}