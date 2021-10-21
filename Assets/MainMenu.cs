using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject menuToOpen;

    private void Start()
    {
        Cursor.visible = true;
    }
    public void StartLevel()
    {
        SceneManager.LoadScene(2);
        
    }

    public void OpenMenu()
    {
        menuToOpen?.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void SetEasyDifficulty()
    {
        Score.SetEasyMode();
    }

    public void SetNormalDifficulty()
    {
        Score.SetNormalMode();
    }

    public void SetHardDifficulty()
    {
        Score.SetHardMode();
    }

    public void StartTutorial()
    {
        SceneManager.LoadScene(1);
    }

    public  void QuitGame()
    {
        Application.Quit();
    }
}
