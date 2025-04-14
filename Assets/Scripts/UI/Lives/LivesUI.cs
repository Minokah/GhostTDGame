using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesUI : MonoBehaviour
{
    public TMP_Text livesCount;
    CanvasVisible canvas;

    void Start()
    {
        livesCount.text = "5";
        canvas = GetComponent<CanvasVisible>();
    }

    public void UpdateCount(int lives)
    {
        livesCount.text = lives.ToString();
    }

    public void Show()
    {
        canvas.Show();
    }

    public void Hide()
    {
        canvas.Hide();
    }
}
