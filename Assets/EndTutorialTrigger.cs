using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTutorialTrigger : MonoBehaviour
{
    TutorialEndMenu tutorialEndMenu;
    PlayerInput input;
    

    public void Start()
    {
        tutorialEndMenu = FindObjectOfType<TutorialEndMenu>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.CompareTag("Player"))
            {
                input = collision.gameObject.GetComponent<PlayerInput>();
                tutorialEndMenu.EnterTutorialEndMenu(input);
            }
        }
    }
}
