using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject howToPlay;
    [SerializeField] GameObject options;

    public static PauseController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void DisplayHowToPlay()
    {
        pauseMenu.SetActive(false);
        mainCanvas.SetActive(false);
        howToPlay.SetActive(true);
        options.SetActive(false);
    }

    public void DisplayOptions()
    {
        pauseMenu.SetActive(false);
        mainCanvas.SetActive(false);
        howToPlay.SetActive(false);
        options.SetActive(true);
    }

    public void DisplayMenuPause()
    {
        pauseMenu.SetActive(true);
        mainCanvas.SetActive(false);
        howToPlay.SetActive(false);
        options.SetActive(false);
    }

    public void UnpauseCanvas()
    {
        mainCanvas.SetActive(true);
        pauseMenu.SetActive(false);
        howToPlay.SetActive(false);
        options.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
