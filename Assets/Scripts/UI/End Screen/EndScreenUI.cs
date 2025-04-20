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
    public TMP_Text bannerText, statisticText, killStat, towerStat, spellStat;
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
            bannerImage.color = new Color(43f/255f, 171f/255f, 28f/255f, 1);
        }
        else {
            bannerText.text = "Defeat...";
            bannerImage.color = new Color(171f / 255, 28f / 255f, 28f / 255f, 1);
        }
        statisticText.text = statText;
    }

    private void Replay() {
        Hide();

		Game.resetGameState();
		Game.playing = true;
		Game.EnemySpawner.SetGameState(true);
		Game.EnemySpawner.SetGameSpawner(Game.currentMap, Game.currentLevel);
		Game.DialogueManager.triggerLevelDialogue(Game.currentMap, Game.currentLevel);
    }

    private void Menu() {
        Game.GameplayCameraController.DisableCams();
        UI.Menu.Show();
        Hide();
		UI.BuildMenu.Hide();
		UI.Spellbar.Hide();
		UI.CameraPanel.Hide();
		UI.LivesPanel.Hide();
    }

    public void Show() {
        Dictionary<string, int> stats = Game.StatisticsManager.session;
        
        // Kills
        string killString = $"Kills: {stats["totalKills"]}\n\n";
        if (stats["basicKills"] > 0) killString += $"Basic: {stats["basicKills"]}\n";
        if (stats["toughKills"] > 0) killString += $"Tough: {stats["toughKills"]}\n";
        if (stats["fastKills"] > 0) killString += $"Fast: {stats["fastKills"]}\n";
        if (stats["machoKills"] > 0) killString += $"Macho: {stats["machoKills"]}\n";
        if (stats["balloonKills"] > 0) killString += $"Balloon: {stats["balloonKills"]}\n";
        if (stats["mixtapeKills"] > 0) killString += $"Mixtape: {stats["mixtapeKills"]}\n";
        if (stats["cameraKills"] > 0) killString += $"Camera: {stats["cameraKills"]}\n";
        killStat.SetText(killString);
        
        // Towers
        string towerString = $"Placed: {stats["towersPlaced"]}\n\n";
        if (stats["boltPlaced"] > 0) towerString += $"Spirit Bolt: {stats["boltPlaced"]}\n";
        if (stats["cannonPlaced"] > 0) towerString += $"Spirit Cannon: {stats["cannonPlaced"]}\n";
        if (stats["sniperPlaced"] > 0) towerString += $"Spirit Sniper: {stats["sniperPlaced"]}\n";
        if (stats["bubblePlaced"] > 0) towerString += $"Spirit Bubble: {stats["bubblePlaced"]}\n";
        if (stats["gatePlaced"] > 0) towerString += $"Spirit Gate: {stats["gatePlaced"]}\n";
        if (stats["gathererPlaced"] > 0) towerString += $"Spirit Gatherer: {stats["gathererPlaced"]}\n";
        if (stats["arcanePlaced"] > 0) towerString += $"Spirit Arcane: {stats["arcanePlaced"]}\n";
        towerStat.SetText(towerString);
        
        // Spells
        string spellString = $"Casted: {stats["spellsCasted"]}\n\n";
        if (stats["fireCasts"] > 0) spellString += $"Agi: {stats["fireCasts"]}\n";
        if (stats["iceCasts"] > 0) spellString += $"Bufu: {stats["iceCasts"]}\n";
        if (stats["lightningCasts"] > 0) spellString += $"Zio: {stats["lightningCasts"]}\n";
        if (stats["windCasts"] > 0) spellString += $"Feng: {stats["windCasts"]}\n";
        if (stats["timeCasts"] > 0) spellString += $"Jikan: {stats["timeCasts"]}\n";
        spellStat.SetText(spellString);
        
        canvas.Show();
    }

    public void Hide() {
        canvas.Hide();
    }
}
