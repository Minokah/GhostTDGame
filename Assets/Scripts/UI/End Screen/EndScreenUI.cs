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
    public Image banner;
    public RawImage bannerImage;
    public TMP_Text bannerText, statisticText;
    public Button replayButton, menuButton;
    public ProfileSectionUI profile;

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
            bannerImage.texture = Resources.Load<Texture>("Icons/Generic/crown");
            banner.color = new Color((float)76/255, (float)209/255, (float)55/255, 1);
        }
        else {
            bannerText.text = "Defeat...";
            bannerImage.texture = Resources.Load<Texture>("Icons/Generic/skull");
            banner.color = new Color((float)140/255, 0, 0, 1);
        }
        statisticText.text = statText;
    }

    private void Replay() {
        Hide();
		UI.BuildMenu.Hide();
		UI.Spellbar.Hide();
		// once level selection UI is finalized, add methods to select the same level again
    }

    private void Menu() {
        Game.GameplayCameraController.DisableCams();
        UI.Menu.Show();
        Hide();
		UI.BuildMenu.Hide();
		UI.Spellbar.Hide();
    }

    public void Show() {
        profile.Set("set rank");
        canvas.Show();
    }

    public void Hide() {
        canvas.Hide();
    }
}
