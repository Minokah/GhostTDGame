using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuStatistics : MonoBehaviour
{
    Game Game;
    UI UI;
    public Button back;
    public TMP_Text kills, towers, spells;
    CanvasVisible canvas;

    void Start()
    {
        Game = Game.Get();
        UI = UI.Get();
        canvas = GetComponent<CanvasVisible>();
        back.onClick.AddListener(Back);
    }

    public void Show()
    {
        Dictionary<string, int> stats = Game.StatisticsManager.statistics;
        
        // Kills
        string killString = $"Total Kills: {stats["totalKills"]}\n\n";
        killString += $"Basic: {stats["basicKills"]}\n";
        killString += $"Tough: {stats["toughKills"]}\n";
        killString += $"Fast: {stats["fastKills"]}\n";
        killString += $"Macho: {stats["machoKills"]}\n";
        killString += $"Balloon: {stats["balloonKills"]}\n";
        killString += $"Mixtape: {stats["mixtapeKills"]}\n";
        killString += $"Camera: {stats["cameraKills"]}\n";
        kills.SetText(killString);
        
        // Towers
        string towerString = $"Total Placed: {stats["towersPlaced"]}\n\n";
        towerString += $"Spirit Bolt: {stats["boltPlaced"]}\n";
        towerString += $"Spirit Cannon: {stats["cannonPlaced"]}\n";
        towerString += $"Spirit Sniper: {stats["sniperPlaced"]}\n";
        towerString += $"Spirit Bubble: {stats["bubblePlaced"]}\n";
        towerString += $"Spirit Gate: {stats["gatePlaced"]}\n";
        towerString += $"Spirit Gatherer: {stats["gathererPlaced"]}\n";
        towerString += $"Spirit Arcane: {stats["arcanePlaced"]}\n";
        towers.SetText(towerString);
        
        // Spells
        string spellString = $"Spells Casted: {stats["spellsCasted"]}\n\n";
        spellString += $"Agi: {stats["fireCasts"]}\n";
        spellString += $"Bufu: {stats["iceCasts"]}\n";
        spellString += $"Zio: {stats["lightningCasts"]}\n";
        spellString += $"Feng: {stats["windCasts"]}\n";
        spellString += $"Jikan: {stats["timeCasts"]}\n";
        spells.SetText(spellString);
        
        canvas.Show();
    }

    public void Hide()
    {
        canvas.Hide();
    }

    private void Back()
    {
        UI.Menu.Show();
    }
}
