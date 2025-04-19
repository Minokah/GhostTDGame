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
    public Button closeButton, stagesButton, cosmeticsButton;
    public CurrencySectionUI currency;
    public GameObject stagesContainer, cosmeticsContainer;

    void Start()
    {
        Game = Game.Get();
        UI = UI.Get();
        canvas = GetComponent<CanvasVisible>();
        closeButton.onClick.AddListener(Hide);
        stagesButton.onClick.AddListener(ShowStages);
        cosmeticsButton.onClick.AddListener(ShowCosmetics);
    }

    public void Show()
    {
        UI.windowActive = true;
        
        ShowStages();
        currency.Refresh();
        canvas.Show();
    }

    public void Hide()
    {
        UI.Menu.Show();
        canvas.Hide();
    }

    private void ShowStages()
    {
        cosmeticsContainer.SetActive(false);
        stagesContainer.SetActive(true);
        foreach (Transform item in stagesContainer.transform)
        {
            item.GetComponent<AchievementMenuEntry>().Refresh();
        }
    }

    private void ShowCosmetics()
    {
        stagesContainer.SetActive(false);
        cosmeticsContainer.SetActive(true);
        foreach (Transform item in cosmeticsContainer.transform)
        {
            item.GetComponent<AchievementMenuEntry>().Refresh();
        }
    }
}
