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
    }
	
    void OnTriggerEnter(Collider collisionObject)
    {
        if (collisionObject.gameObject.tag == "Enemy")
        {
			Debug.Log("Enemy Triggered This");
			Game.playing = false;
			UI.EndScreen.Show();
            UI.EndScreen.SetState(false, "This is a test text from Henry");
        }
    }
}
