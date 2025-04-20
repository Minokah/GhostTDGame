using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestProfileLoader : MonoBehaviour
{
    Game Game;
    public Button loadButton, saveButton;

    // Start is called before the first frame update
    void Start()
    {
        Game = Game.Get();
        loadButton.onClick.AddListener(Load);
        saveButton.onClick.AddListener(Save);
    }

    // Update is called once per frame
    void Load()
    {
        Game.ProfileManager.Load();
    }

    void Save()
    {
        Game.ProfileManager.Save();
    }
}
