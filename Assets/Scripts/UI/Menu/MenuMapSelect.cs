using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuMapSelect : MonoBehaviour
{
    CanvasVisible canvas;
    public Button menu, back, next, one, two, three, play;
    public TMP_Text mapName;
    UI UI;

    int selectedMap = 0;
    int selectedStage = 0;

    void Start()
    {
        UI = UI.Get();
        canvas = GetComponent<CanvasVisible>();
        menu.onClick.AddListener(Menu);
        play.onClick.AddListener(Play);
        back.onClick.AddListener(PreviousMap);
        next.onClick.AddListener(NextMap);
    }

    public void Show()
    {
        canvas.Show();
    }

    public void Hide()
    {
        canvas.Hide();
    }

    private void UpdatePreview()
    {
        switch (selectedMap)
        {
            case 0:
                mapName.text = "Map 1";
                break;
            case 1:
                mapName.text = "Map 2";
                break;
            case 2:
                mapName.text = "Map 3";
                break;
            case 3:
                mapName.text = "Map 4";
                break;
        }
    }

    private void NextMap()
    {
        selectedStage = 0;
        selectedMap++;
        if (selectedMap > 3) selectedMap = 0;
        UpdatePreview();
    }

    private void PreviousMap()
    {
        selectedStage = 0;
        selectedMap--;
        if (selectedMap < 0) selectedMap = 3;
        UpdatePreview();
    }

    private void Play()
    {
        UI.Menu.Play(selectedMap, selectedStage);
    }

    private void Menu()
    {
        UI.Menu.Show();
    }
}
