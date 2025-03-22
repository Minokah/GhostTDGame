using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenuUI : MonoBehaviour
{
    Game Game;
    CanvasVisible canvas;
    public BuildMenuEntry bolt, bomb, sniper, bubble, gate, gatherer, arcane;

    // Start is called before the first frame update
    void Start()
    {
        Game = Game.Get();
        canvas = GetComponent<CanvasVisible>();
    }

    public void Show() {
        RefreshUnlockState();
        canvas.Show();
    }

    public void Hide() {
        canvas.Hide();
    }

    // Refresh unlock state.
    public void RefreshUnlockState()
    {
        bolt.SetUnlocked(GetUnlockState(bolt));
        bomb.SetUnlocked(GetUnlockState(bomb));
        sniper.SetUnlocked(GetUnlockState(sniper));
        bubble.SetUnlocked(GetUnlockState(bubble));
        gate.SetUnlocked(GetUnlockState(gate));
        gatherer.SetUnlocked(GetUnlockState(gatherer));
        arcane.SetUnlocked(GetUnlockState(arcane));
    }

    private bool GetUnlockState(BuildMenuEntry entry)
    {
        return Game.StatisticsManager.statistics["levelCount"] >= entry.entity.GetComponent<ProgressionEntry>().requireLevel;
    }
}
