using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoodGameOver : MonoBehaviour
{
	UI UI;
	Game Game;
	public AudioClip GameOverSFX;
	void Start()
    {
		Game = Game.Get();
        UI = UI.Get();
    }
	
    public void WonLevel()
    {

		AudioManager.Instance.PlaySFX(GameOverSFX, 0.01f);
        Game.playing = false;
		Game.EnemySpawner.SetGameState(false);
		UI.EndScreen.Show();
		string unlockMessage = "";
		bool newUnlocks = false;
		
		Game.StatisticsManager.addTokens(3);
		unlockMessage = unlockMessage + "You have earned 3 upgrade tokens.\n";	
		unlockMessage = unlockMessage + "\n";
		
		// Check if achievement should be unlocked and if so unlock it
		if (Game.currentMap == 0 && Game.currentLevel == 1){
			Game.AchievementManager.GrantAchievement("Map1");
		}
		else if (Game.currentMap == 1 && Game.currentLevel == 2){
			Game.AchievementManager.GrantAchievement("Map2");
		}
		else if (Game.currentMap == 2 && Game.currentLevel == 2){
			Game.AchievementManager.GrantAchievement("Map3");
		}
		else if (Game.currentMap == 3 && Game.currentLevel == 1){
			Game.AchievementManager.GrantAchievement("Map4");
			
			// Grant cosmetics for all towers
			foreach (GameObject entry in Game.ProgressionManager.towerList.Values)
			{
				entry.GetComponents<ProgressionCosmetic>()[0].unlocked = true;
			}
		}
		
		// Check if new content should be unlocked
		if (Game.currentMap == 0 && Game.currentLevel == Game.StatisticsManager.getLevel()){
			newUnlocks = true;
		}
		else if (Game.currentMap == 1 && (Game.currentLevel + 2) == Game.StatisticsManager.getLevel()){
			newUnlocks = true;
		}
		else if (Game.currentMap == 2 && (Game.currentLevel + 5) == Game.StatisticsManager.getLevel()){
			newUnlocks = true;
		}
		else if (Game.currentMap == 3 && (Game.currentLevel + 8) == Game.StatisticsManager.getLevel()){
			newUnlocks = true;
		}

		// If player actually unlocked any new content
		if (newUnlocks == true){
			Game.StatisticsManager.addLevel(1);
			unlockMessage = unlockMessage + "You can now play the next level or map.\n";
			unlockMessage = unlockMessage + "\n";
			unlockMessage = unlockMessage + "You have earned a new spell or tower! Check out the \"Towers and Spells.\" tab to view it.\n";
		}
		
		UI.EndScreen.SetState(true, unlockMessage);

        Game.ProfileManager.Save();

        Game.resetGameState();
		UI.resetLevelSelection();
    }
}
