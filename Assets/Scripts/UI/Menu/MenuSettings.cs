using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSettings : MonoBehaviour
{
    UI UI;
    public Button back;
    CanvasVisible canvas;
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        UI = UI.Get();
        canvas = GetComponent<CanvasVisible>();

        if (back != null){
            back.onClick.AddListener(Back);
        }
            

        musicSlider.onValueChanged.RemoveAllListeners();
        sfxSlider.onValueChanged.RemoveAllListeners();

        // initialize slider positions from saved prefs
        float m = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        musicSlider.value = Mathf.Clamp(m, musicSlider.minValue, musicSlider.maxValue);

        float s = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        sfxSlider.value   = Mathf.Clamp(s, sfxSlider.minValue,   sfxSlider.maxValue);

        Debug.Log(
        $"MusicSlider: range [{musicSlider.minValue}…{musicSlider.maxValue}], " +
        $"loading {PlayerPrefs.GetFloat("MusicVolume",0.75f)}"
        );
        // wire up events to AudioManager
        musicSlider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
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
