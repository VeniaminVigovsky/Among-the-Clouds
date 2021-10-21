using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{

    protected static bool isPaused = false;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] PlayerInput playerInput;
    
    public virtual void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                HandlePause();
            else if (isPaused)
                ResumeGame();
        }
    }

    public void HandlePause()
    {
        if (TutorialMenuScript.tutorialPause) return;
        PauseGame();
    }
    public virtual void PauseGame()
    {
        pauseMenu?.SetActive(true);
        playerInput.enabled = false;
        isPaused = true;
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        if (TutorialMenuScript.tutorialPause) return;
        pauseMenu?.SetActive(false);
        playerInput.enabled = true;
        isPaused = false;
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    public virtual void QuitGame()
    {
        ResumeGame();
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            Score.UpdateBestScore();
        }
        Score.score = 0;
        SceneManager.LoadScene(0);
    }

}
