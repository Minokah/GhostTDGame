using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public GameObject camera;
    public Button quit, progression, play, achievements, credits;
    public CanvasVisible creditsPanel;
    CanvasVisible canvas;
    UI UI;
    Game Game;

    bool showingCredits = false;

    void Start()
    {
        canvas = GetComponent<CanvasVisible>();
        UI = UI.Get();
        Game = Game.Get();
        quit.onClick.AddListener(QuitGame);
        progression.onClick.AddListener(ShowProgression);
        achievements.onClick.AddListener(ShowAchievements);
        play.onClick.AddListener(PlayGame);
        credits.onClick.AddListener(ToggleCredits);
    }

    public void Show()
    {
        camera.SetActive(true);
        canvas.Show();
    }

    public void Hide()
    {
        canvas.Hide();
    }

    public void PlayGame()
    {
        Hide();
        UI.windowActive = false;
        UI.Spellbar.Refresh();
        Game.EnemySpawner.SetGameState(true);
        Game.GameplayCameraController.EnableCams();
        camera.SetActive(true);
    }

    public void ShowProgression()
    {
        canvas.Hide();
        UI.ProgressionMenu.Show();
    }

    public void ShowAchievements()
    {
        canvas.Hide();
        UI.AchievementMenu.Show();
    }

    public void ToggleCredits()
    {
        showingCredits = !showingCredits;
        if (!showingCredits) creditsPanel.Hide();
        else creditsPanel.Show();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
