using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPointFullControl : EventPoint
{

    
    public override void Start()
    {
        base.Start();
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
