using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuMain : MonoBehaviour
{
    public Button quit, progression, play, achievements, statistics, credits, reset, settings;
    public TMP_Text resetText;
    CanvasVisible canvas;
    UI UI;
    Game Game;

    bool resetting = false;

    void Start()
    {
        canvas = GetComponent<CanvasVisible>();
        UI = UI.Get();
        Game = Game.Get();
        quit.onClick.AddListener(QuitGame);
        progression.onClick.AddListener(ShowProgression);
        achievements.onClick.AddListener(ShowAchievements);
        credits.onClick.AddListener(ShowCredits);
        play.onClick.AddListener(PlayGame);
        reset.onClick.AddListener(BeginReset);
        settings.onClick.AddListener(ShowSettings);
        statistics.onClick.AddListener(ShowStatistics);
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

    public void ShowCredits()
    {
        UI.Menu.ShowCredits();
    }
    public void ShowSettings()
    {
        UI.Menu.ShowSettings();
    }

    public void ShowStatistics()
    {
        UI.Menu.ShowStatistics();
    }

    public void BeginReset()
    {
        if (!resetting)
        {
            resetting = true;
            resetText.text = "Click again to confirm save reset";
        }
        else
        {
            Game.ProfileManager.ResetSave();
            resetting = false;
            resetText.text = "Reset Save Data";
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
