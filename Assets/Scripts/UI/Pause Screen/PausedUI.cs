using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausedUI : MonoBehaviour
{
    public GameObject main, confirm;
    public Button resume, quit, yes, no;
    CanvasVisible canvas;
    Game Game;

    // Start is called before the first frame update
    void Start()
    {
        Game = Game.Get();
        canvas = GetComponent<CanvasVisible>();
        resume.onClick.AddListener(Resume);
        quit.onClick.AddListener(ShowConfirm);
        yes.onClick.AddListener(Quit);
        no.onClick.AddListener(ShowMenu);
    }

    void Update()
    {
        // ESC to pause/unpause, only allow in game
        if (Game.playing && Input.GetKeyUp(KeyCode.Escape))
        {
            Game.paused = !Game.paused;

            if (Game.paused) Show();
            else Hide();
        }
    }

    public void Show()
    {
        ShowMenu();
        canvas.Show(1);
        Time.timeScale = 0;
    }

    public void Hide()
    {
        canvas.Hide();
        Time.timeScale = 1;
    }

    private void Resume()
    {
        Hide();
        Game.paused = false;
    }

    private void ShowConfirm()
    {
        main.SetActive(false);
        confirm.SetActive(true);
    }

    private void ShowMenu()
    {
        main.SetActive(true);
        confirm.SetActive(false);
    }

    // Load the menu scene
    private void Quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Scence");
    }
}
