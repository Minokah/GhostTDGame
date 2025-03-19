using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenUI : MonoBehaviour
{
    Game Game;
    UI UI;
    CanvasVisible canvas;
    public Image bannerImage;
    public TMP_Text bannerText, statisticText;
    public Button replayButton, menuButton;

    // Start is called before the first frame update
    void Start()
    {
        Game = Game.Get();
        UI = UI.Get();
        canvas = GetComponent<CanvasVisible>();
        replayButton.onClick.AddListener(Replay);
        menuButton.onClick.AddListener(Menu);
    }

    public void SetState(bool victory, string statText = "Stats text") {
        if (victory) {
            bannerText.text = "Stage Cleared!";
            bannerImage.color = new Color(43/255, 171/255, 28/255, 1);
        }
        else {
            bannerText.text = "Defeat...";
            bannerImage.color = new Color(171 / 255, 28 / 255, 28 / 255, 1);
        }
        statisticText.text = statText;
    }

    private void Replay() {
        Hide();

		Game.resetGameState();
		Game.playing = true;
		Game.EnemySpawner.SetGameState(true);
		Game.EnemySpawner.SetGameSpawner(Game.currentMap, Game.currentLevel, 1); // challange mode is off(2 for on); We will replace this latter with dynamic values based on which button the user clicks 
		Game.DialogueManager.triggerLevelDialogue(Game.currentMap, Game.currentLevel);
    }

    private void Menu() {
        Game.GameplayCameraController.DisableCams();
        UI.Menu.Show();
        Hide();
		UI.BuildMenu.Hide();
		UI.Spellbar.Hide();
    }

    public void Show() {
        canvas.Show();
    }

    public void Hide() {
        canvas.Hide();
    }
}
