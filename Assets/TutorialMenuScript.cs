using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMenuScript : MonoBehaviour
{
    
    [SerializeField]
    Text tutorialDescription;
    EventPoint point;
    [SerializeField]
    Image tutorialMenuCanvas;
    PlayerInput input;
    [SerializeField]
    PauseMenuScript pauseMenu;

    Button tutorialButton;
    WaitForSecondsRealtime timeBeforeEnable = new WaitForSecondsRealtime(0.8f);

    public static bool tutorialPause = false;

    private void Start()
    {
        tutorialButton = tutorialMenuCanvas.GetComponentInChildren<Button>();
        tutorialButton.gameObject.SetActive(false);

        tutorialButton.onClick.AddListener(() => 
        {
            point?.gameObject.SetActive(false);
            Time.timeScale = 1;
            Cursor.visible = false;
            if (input != null) input.enabled = true;
            if (pauseMenu != null) pauseMenu.enabled = true;
            tutorialButton.gameObject.SetActive(false);
            tutorialMenuCanvas?.gameObject.SetActive(false);
            StopAllCoroutines();
            tutorialPause = false;
           

        });

        tutorialMenuCanvas.gameObject.SetActive(false);
    }
    public void EnterTutorial(string tutorialText, EventPoint point, PlayerInput input)
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        if (pauseMenu != null) pauseMenu.enabled = false;
        this.point = point;
        this.input = input;
        if (input != null) input.enabled = false;
        tutorialDescription.text = tutorialText;
        tutorialPause = true;
        StartCoroutine(EnableButton());
        tutorialMenuCanvas.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) return;
    }

    private void OnDestroy()
    {
        tutorialPause = false;
    }

    private IEnumerator EnableButton()
    {
        yield return timeBeforeEnable;
        tutorialButton.gameObject.SetActive(true);
        yield break;
    }

}
