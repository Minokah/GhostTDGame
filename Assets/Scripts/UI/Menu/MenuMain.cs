using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuMain : MonoBehaviour
{
    public Button quit, progression, play, achievements;
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
    }

    public void Show()
    {
        canvas.Show();
    }

    public void Hide()
    {
        canvas.Hide();
    }

    public void PlayGame()
    {
        UI.Menu.ShowMaps();
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

    public void QuitGame()
    {
        Application.Quit();
    }
}
