using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenuUI : MonoBehaviour
{
    CanvasVisible canvas;
    public BuildMenuEntry bolt, bomb, sniper, bubble, gate, gatherer, arcane;

    // Start is called before the first frame update
    void Start()
    {
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
        bolt.SetUnlocked(bolt.entity.GetComponent<ProgressionEntry>().unlocked);
        bomb.SetUnlocked(bomb.entity.GetComponent<ProgressionEntry>().unlocked);
        sniper.SetUnlocked(sniper.entity.GetComponent<ProgressionEntry>().unlocked);
        bubble.SetUnlocked(bubble.entity.GetComponent<ProgressionEntry>().unlocked);
        gate.SetUnlocked(gate.entity.GetComponent<ProgressionEntry>().unlocked);
        gatherer.SetUnlocked(gatherer.entity.GetComponent<ProgressionEntry>().unlocked);
        arcane.SetUnlocked(arcane.entity.GetComponent<ProgressionEntry>().unlocked);
    }
}
