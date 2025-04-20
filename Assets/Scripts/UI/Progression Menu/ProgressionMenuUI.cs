using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionMenuUI : MonoBehaviour
{
    Game Game;
    UI UI;
    CanvasVisible canvas;
    public Button closeButton;
    public CurrencySectionUI currency;
    public ProgressionMenuListing listing;
    public ProgressionMenuInfo info;
    public GameObject camera;
    private GameObject previousCamera;

    void Start()
    {
        Game = Game.Get();
        UI = UI.Get();
        canvas = GetComponent<CanvasVisible>();
        closeButton.onClick.AddListener(Hide);
    }

    public void ShowInformation(GameObject entry, GameObject preview, GameObject nowCamera)
    {
        if (previousCamera != null) previousCamera.SetActive(false);
        previousCamera = nowCamera;
        nowCamera.SetActive(true);
        camera.SetActive(false);
        listing.Hide();
        info.Show(entry, preview);
    }

    // Shows the main menu listing
    public void Show()
    {
        currency.Refresh();
        camera.SetActive(true);
        UI.windowActive = true;
        canvas.Show();
        listing.Show();
        info.Hide();
    }

    public void Hide()
    {
        // If the user is selecting a spell, go back to the default mode
        UI.ProgressionMenu.listing.StopSpellChange();
        if (previousCamera != null) previousCamera.SetActive(false);
        previousCamera = null;
        camera.SetActive(true);
        UI.Menu.Show();
        canvas.Hide();
    }
}
