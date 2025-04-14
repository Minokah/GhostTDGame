using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BadGameOver : MonoBehaviour
{
	UI UI;
	Game Game;

	void Start()
    {
		Game = Game.Get();
        UI = UI.Get();
		Game.lives = 5;
    }
	
    void OnTriggerEnter(Collider collisionObject)
    {
        if (collisionObject.gameObject.tag == "Enemy")
        {
			Game.lives = Game.lives - 1;
			UI.LivesPanel.UpdateCount(Game.lives);
			if	(Game.lives <= 0){
				Game.playing = false;
				Game.EnemySpawner.SetGameState(false);
				
				UI.EndScreen.Show();
				UI.EndScreen.SetState(false, "Game Over!");
				
				Game.resetGameState();
				UI.resetLevelSelection();
			}
        }
    }
}
