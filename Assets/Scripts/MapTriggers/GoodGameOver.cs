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
        UI.EndScreen.SetState(true, "This is a test victory text from Henry");
		// add more stuff to player level and upgrade tokens
		Game.resetGameState();
    }
}
