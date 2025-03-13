using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuMapSelect : MonoBehaviour
{
    CanvasVisible canvas;
    public Button back, testmap;
    UI UI;

    void Start()
    {
        UI = UI.Get();
        canvas = GetComponent<CanvasVisible>();
        back.onClick.AddListener(Back);
        testmap.onClick.AddListener(PlayTest);
    }

    public void Show()
    {
        canvas.Show();
    }

    public void Hide()
    {
        canvas.Hide();
    }

    private void PlayTest()
    {
        UI.Menu.Play();
    }

    private void Back()
    {
        UI.Menu.Show();
    }
}
