using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CameraModeUI : MonoBehaviour
{
    Game Game;
    public Button overheadBtn, pivotBtn;
    public TMP_Text infoText;
    CanvasVisible canvas;

    void Start()
    {
        Game = Game.Get();
        overheadBtn.onClick.AddListener(SetOverhead);
        pivotBtn.onClick.AddListener(SetPivot);
        canvas = GetComponent<CanvasVisible>();
    }

    void Update()
    {
        // V key will toggle between them without popping open menu
        if (Input.GetKeyDown(KeyCode.V)) {
            if (Game.GameplayCameraController.overhead) SetPivot();
            else SetOverhead();
        }
    }

    // Change to overhead camera mode
    private void SetOverhead() {
        Game.GameplayCameraController.SetOverhead(true);
        SetColours(overheadBtn, pivotBtn);
        infoText.text = "Top down camera to see the whole area.";
    }

    // Change to pivot camera mode
    private void SetPivot() {
        Game.GameplayCameraController.SetOverhead(false);
        SetColours(pivotBtn, overheadBtn);
        infoText.text = "Pannable camera to get a closer view.\n WASD to pan camera in Direction.\n Hold Right-Click and Move the Mouse to zoom in-an-out.";
    }

    // Set the colours of the buttons
    private void SetColours(Button on, Button off) {
        ColorBlock onClrs = overheadBtn.colors;
        onClrs.normalColor = new Color(0/255f, 187f/255f, 255f/255f);
        on.colors = onClrs;

        ColorBlock offClrs = pivotBtn.colors;
        offClrs.normalColor = new Color(100f/255f, 100f/255f, 100f/255f);
        off.colors = offClrs;
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
