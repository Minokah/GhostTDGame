using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoodGameOver : MonoBehaviour
{
	UI UI;
	Game Game;
	void Start()
    {
		Game = Game.Get();
        UI = UI.Get();
    }
	
    public void WonLevel()
    {
        Game.playing = false;
		Game.EnemySpawner.SetGameState(false);
		UI.EndScreen.Show();
		string unlockMessage = "";

		
		// Unlock new content
		Game.StatisticsManager.addLevel(1);
		unlockMessage = unlockMessage + "You have earned an additional progress level.\n";
		if (Game.challangeMode != 0){
			Game.StatisticsManager.addTokens(6);
			unlockMessage = unlockMessage + "You have earned 6 upgrade tokens.\n";
		}
		else{
			Game.StatisticsManager.addTokens(3);
			unlockMessage = unlockMessage + "You have earned 3 upgrade tokens.\n";
		}	
		if (Game.currentMap == 0){
			if (Game.currentLevel == 1){
				Game.AchievementManager.GrantAchievement("Map1");
			}
		}
		else if (Game.currentMap == 1){
			if (Game.currentLevel == 2){
				Game.AchievementManager.GrantAchievement("Map2");
			}
		}
		else if (Game.currentMap == 2){
			if (Game.currentLevel == 2){
				Game.AchievementManager.GrantAchievement("Map3");
			}
		}
		else if (Game.currentMap == 3){
			if (Game.currentLevel == 1){
				Game.AchievementManager.GrantAchievement("Map4");
			}
		}		
		
		unlockMessage = unlockMessage + "You can now play the next level or map.\n";
		UI.EndScreen.SetState(true, unlockMessage);
		
		Game.resetGameState();
		UI.resetLevelSelection();
    }
}
