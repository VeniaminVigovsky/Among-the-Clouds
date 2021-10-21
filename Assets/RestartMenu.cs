using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartMenu : PauseMenuScript
{
    [SerializeField] GameObject cam;

    [SerializeField] GameObject restartMenu;

    [SerializeField] PlayerDeath playerDeath;

    Vector3 camStartPosition;
    public override void Start()
    {
        base.Start();

        camStartPosition = cam.transform.position;

        playerDeath.PlayerDead += ActivateRestartMenu;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ActivateRestartMenu()
    {
        PauseGame();
        restartMenu.SetActive(true);
    }

    public override void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        restartMenu.SetActive(false);
        isPaused = false;
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            Score.UpdateBestScore();
        }
        Score.score = 0;
        PlayerDeath.liveCount = 5;
        playerDeath.PlayerDead -= ActivateRestartMenu;
        PlayerMovement.stopMovement = false;
        Score.canUpdateScore = true;
        StartCoroutine(Score.ScoreUpdate());
        Score.dead = false;
        cam.transform.position = camStartPosition;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public override void QuitGame()
    {
        base.QuitGame();
    }


}
