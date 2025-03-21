using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCredits : MonoBehaviour
{
    UI UI;
    public Button back;
    CanvasVisible canvas;

    void Start()
    {
        UI = UI.Get();
        canvas = GetComponent<CanvasVisible>();
        back.onClick.AddListener(Back);
    }

    public void Show()
    {
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
