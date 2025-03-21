using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject optionCanvas;
    [SerializeField] GameObject howToPlay;

    public void MainScreen()
    {
        mainCanvas.SetActive(true);
        howToPlay.SetActive(false);
        optionCanvas.SetActive(false);
    }
    public void OptionScreen()
    {
        mainCanvas.SetActive(false);
        howToPlay.SetActive(false);
        optionCanvas.SetActive(true);
    }

    public void HowtoPlayScreen()
    {
        mainCanvas.SetActive(false);
        howToPlay.SetActive(true);
        optionCanvas.SetActive(false);
    }

    public void LaunchGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
