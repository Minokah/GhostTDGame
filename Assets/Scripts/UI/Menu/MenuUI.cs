using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public GameObject camera;
    public MenuMain main;
    public MenuMapSelect mapSelect;
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

    public void Play()
    {
        Game.playing = true;
        Hide();
        UI.windowActive = false;
        UI.Spellbar.Refresh();
        Game.EnemySpawner.SetGameState(true);
        Game.GameplayCameraController.EnableCams();
        camera.SetActive(false);
    }
}
