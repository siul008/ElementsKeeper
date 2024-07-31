using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject howToPlay;
    [SerializeField] GameObject options;

    [Header("Post Processing")] 
    [SerializeField] private VolumeProfile normalVolume;
    [SerializeField] private VolumeProfile blurVolume;
    [SerializeField] private Volume postProcess;

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
        gameOver.SetActive(false);
    }

    public void DisplayOptions()
    {
        pauseMenu.SetActive(false);
        mainCanvas.SetActive(false);
        howToPlay.SetActive(false);
        options.SetActive(true);
        gameOver.SetActive(false);
        postProcess.profile = blurVolume;
    }

    public void DisplayMenuPause()
    {
        pauseMenu.SetActive(true);
        mainCanvas.SetActive(false);
        howToPlay.SetActive(false);
        options.SetActive(false);
        gameOver.SetActive(false);
        postProcess.profile = blurVolume;
    }
    
    public void DisplayGameOver()
    {
        gameOver.SetActive(true);
        mainCanvas.SetActive(false);
        howToPlay.SetActive(false);
        options.SetActive(false);
        postProcess.profile = blurVolume;
        Time.timeScale = 0;
    }

    public void UnpauseCanvas()
    {
        mainCanvas.SetActive(true);
        pauseMenu.SetActive(false);
        howToPlay.SetActive(false);
        options.SetActive(false);
        gameOver.SetActive(false);
        postProcess.profile = normalVolume;
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(0);
    }
}
