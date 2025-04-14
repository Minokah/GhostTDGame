using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public GameObject camera;
    public MenuMain main;
    public MenuMapSelect mapSelect;
    public MenuCredits credits;
    CanvasVisible canvas;
    UI UI;
    Game Game;

    void Start()
    {
        UI = UI.Get();
        Game = Game.Get();
        canvas = GetComponent<CanvasVisible>();
    }

    public void Show()
    {
        main.Show();
        credits.Hide();
        mapSelect.Hide();
        camera.SetActive(true);
        canvas.Show();
    }

    public void Hide()
    {
        canvas.Hide();
    }

    public void ShowMaps()
    {
        main.Hide();
        mapSelect.Show();
    }

    public void ShowCredits()
    {
        main.Hide();
        credits.Show();
    }

    public void Play(int map, int stage)
    {
        Game.playing = true;
        Hide();
        UI.windowActive = false;
        UI.Spellbar.Refresh();
		
		Game.currentMap = map;
		Game.currentLevel = stage;
		
        Game.EnemySpawner.SetGameState(true);
        Game.EnemySpawner.SetGameSpawner(map, stage);
		Game.DialogueManager.triggerLevelDialogue(map, stage);

        Game.LevelManager.loadMap(map);


		UI.CameraPanel.Show();
		UI.LivesPanel.Show();
        UI.BuildMenu.Show();
		UI.Spellbar.Show();
        Game.GameplayCameraController.EnableCams();
        camera.SetActive(false);
    }
}
