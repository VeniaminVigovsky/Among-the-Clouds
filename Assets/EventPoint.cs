using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPoint : MonoBehaviour
{
    protected TutorialMenuScript tutorialMenuScript;
    protected PlayerInputNoGravity inputNoGravity;
    [SerializeField] [TextArea(3, 5)]
    protected string tutorialText;

    public virtual void Start()
    {
        tutorialMenuScript = FindObjectOfType<TutorialMenuScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.CompareTag("Player"))
            {
                inputNoGravity = collision.gameObject.GetComponent<PlayerInputNoGravity>();
                tutorialMenuScript?.EnterTutorial(tutorialText, this, inputNoGravity);
            }
        }
    }
}
