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
    public EnemySpawner EnemySpawner;
	public DialogueManager DialogueManager;
	public LevelManager LevelManager;

    public bool paused = false;
    public bool playing = false;
	public int currentMap = 0;
	public int currentLevel = 0;
	public int lives = 5;
	
	UI UI;
	
	void Start()
    {
        UI = UI.Get();
    }

    public static Game Get() {
        return GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
    }
	
	public void resetGameState(){
		lives = 5;
		UI.LivesPanel.UpdateCount(lives);
		StatisticsManager.ResetLevelMoney();
		SpellManager.resetSpellManager();
		TowerPlacementManager.resetTowerPlacementManager();
		EnemySpawner.resetEnemySpawner();
		DialogueManager.resetDialogue();
	}	
}