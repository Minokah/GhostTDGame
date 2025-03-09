using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AchievementMenuUI : MonoBehaviour
{
    Game Game;
    UI UI;
    CanvasVisible canvas;
    public GameObject contentContainer;
    public Button closeButton;
    public ProfileSectionUI profile;

    void Start()
    {
        Game = Game.Get();
        UI = UI.Get();
        canvas = GetComponent<CanvasVisible>();
        closeButton.onClick.AddListener(Hide);
    }

    public void Show()
    {
        UI.windowActive = true;

        // Update everything
        foreach (Transform item in contentContainer.transform)
        {
            item.GetComponent<AchievementMenuEntry>().Refresh();
        }

        canvas.Show();
    }

    public void Hide()
    {
        UI.Menu.Show();
        canvas.Hide();
    }
}
