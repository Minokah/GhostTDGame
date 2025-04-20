using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BadGameOver : MonoBehaviour
{
	UI UI;
	Game Game;
	public AudioClip GameOverSFX;

	void Start()
    {
		Game = Game.Get();
        UI = UI.Get();
		Game.lives = 5;
    }

	void Update(){
			// test the Game Over SFX (and full logic) by pressing I
        if (Input.GetKeyDown(KeyCode.I))
        {
            DoGameOver();
        }
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
				AudioManager.Instance.PlaySFX(GameOverSFX, 0.04f);
				
				UI.EndScreen.Show();
				UI.EndScreen.SetState(false, "Game Over!");
				
				Game.resetGameState();
				UI.resetLevelSelection();
			}
        }
    }

	private void DoGameOver()
    {
        // 1) stop gameplay
        Game.playing = false;
        Game.EnemySpawner.SetGameState(false);

        // 2) play the SFX (at low volume)
        AudioManager.Instance.PlaySFX(GameOverSFX, 0.04f);

        // 3) show end screen
        UI.EndScreen.Show();
        UI.EndScreen.SetState(false, "Game Over!");

        // 4) reset for next run
        Game.resetGameState();
        UI.resetLevelSelection();
    }
}
