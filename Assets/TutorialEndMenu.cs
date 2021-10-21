using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialEndMenu : MonoBehaviour
{
    [SerializeField]
    Image tutorialMenuCanvas;
    PlayerInput input;
    [SerializeField]
    PauseMenuScript pauseMenu;

    private void Start()
    {
        tutorialMenuCanvas.gameObject.SetActive(false);
    }

    public void EnterTutorialEndMenu(PlayerInput input)
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        if (pauseMenu != null) pauseMenu.enabled = false;
        if (input != null) input.enabled = false;
        this.input = input;
        tutorialMenuCanvas.gameObject.SetActive(true);
    }

    public void QuitToMenu()
    {
        CloseTutorialEnd();
        SceneManager.LoadScene(0);
    }

    public void RestartTutorial()
    {
        CloseTutorialEnd();
        SceneManager.LoadScene(1);
    }

    public void StartMainGame()
    {
        CloseTutorialEnd();
        SceneManager.LoadScene(2);
    }

    private void CloseTutorialEnd()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        if (pauseMenu != null) pauseMenu.enabled = true;
        if (input != null) input.enabled = true;
        tutorialMenuCanvas.gameObject.SetActive(false);
    }
}
