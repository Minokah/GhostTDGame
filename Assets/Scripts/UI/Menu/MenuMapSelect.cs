using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuMapSelect : MonoBehaviour
{
    CanvasVisible canvas;
    public Button menu, back, next, one, two, three, play;
    public TMP_Text mapName, stage;
    public GameObject locked;
    UI UI;
    Game Game;

    int selectedMap = 0;
    int selectedStage = 0;

    void Start()
    {
        Game = Game.Get();
        UI = UI.Get();
        canvas = GetComponent<CanvasVisible>();
        menu.onClick.AddListener(Menu);
        play.onClick.AddListener(Play);
        back.onClick.AddListener(PreviousMap);
        next.onClick.AddListener(NextMap);
        one.onClick.AddListener(SetStageOne);
        two.onClick.AddListener(SetStageTwo);
        three.onClick.AddListener(SetStageThree);
        UpdatePreview();
    }

    public void Show()
    {
        canvas.Show();
    }

    public void Hide()
    {
        canvas.Hide();
    }

    public void UpdatePreview()
    {
        SetStageOne();
        one.gameObject.SetActive(false);
        two.gameObject.SetActive(false);
        three.gameObject.SetActive(false);
        locked.SetActive(true);
        play.interactable = false;

        int level = Game.StatisticsManager.statistics["levelCount"];
		//int level = 10;

        switch (selectedMap)
        {
            case 0:
                mapName.text = "Map 1";
                if (level >= 0)
                {
                    one.gameObject.SetActive(true);
                    locked.SetActive(false);
                    play.interactable = true;
                }
                if (level >= 1) two.gameObject.SetActive(true);
                break;
            case 1:
                mapName.text = "Map 2";
                if (level >= 2)
                {
                    one.gameObject.SetActive(true);
                    locked.SetActive(false);
                    play.interactable = true;
                }
                if (level >= 3) two.gameObject.SetActive(true);
                if (level >= 4) three.gameObject.SetActive(true);
                break;
            case 2:
                mapName.text = "Map 3";
                if (level >= 5)
                {
                    one.gameObject.SetActive(true);
                    locked.SetActive(false);
                    play.interactable = true;
                }
                if (level >= 6) two.gameObject.SetActive(true);
                if (level >= 7) three.gameObject.SetActive(true);
                break;
            case 3:
                mapName.text = "Map 4";
                if (level >= 8)
                {
                    one.gameObject.SetActive(true);
                    locked.SetActive(false);
					play.interactable = true;
                }
                if (level >= 9) two.gameObject.SetActive(true);
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
		Debug.Log("Selected Map " + selectedMap);
		Debug.Log("Selected Stage " + selectedStage);
        UI.Menu.Play(selectedMap, selectedStage);
    }

    private void Menu()
    {
        UI.Menu.Show();
    }

    private void SetStageOne()
    {
        selectedStage = 0;
        stage.text = "Stage 1";
    }

    private void SetStageTwo()
    {
        selectedStage = 1;
        stage.text = "Stage 2";
    }

    private void SetStageThree()
    {
        selectedStage = 2;
        stage.text = "Stage 3";
    }
}
