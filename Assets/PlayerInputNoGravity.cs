using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputNoGravity : PlayerInput
{

    bool canChangeGravity;
    [SerializeField] 
    Transform gravityChangingBorder;
      
    public override void Start()
    {
        base.Start();
        
    }

    // Update is called once per frame
    void Update()
    {

        canChangeGravity = gravityChangingBorder != null ? transform.position.x > gravityChangingBorder.position.x : false; 

        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);

            foreach (Touch touch in Input.touches)
            {
                int id = touch.fingerId;
                if (EventSystem.current.IsPointerOverGameObject(id))
                {
                    touchingUI = true;
                    break;
                }

                else touchingUI = false;

            }


            if (!touchingUI)
            {
                HandleJump();
                if (canChangeGravity)
                    HandleGravityChange();
            }



        }

        HandleJumpKeyboard();
        if (canChangeGravity)
            HandleGravityChangeKeyboard();


    }


    //touch input
    private void HandleJump()
    {

        if (Input.touchCount == 1 && touch.phase == TouchPhase.Began && !jumpPressed && playerMovement.isTouchingGround)
        {
            jumpPressed = true;
            playerMovement.jumpTimeRemained = playerMovement.maxJumpTime;
        }

        if (Input.touchCount == 1 && touch.phase == TouchPhase.Ended || playerMovement.jumpTimeRemained < 0)
        {
            jumpPressed = false;
        }




    }

    //touch input
    private void HandleGravityChange()
    {
        if (Input.touchCount == 1 && touch.phase == TouchPhase.Began && !playerMovement.isTouchingGround)
        {
            playerMovement.attemtToChangeGravity = true;
        }


    }

    //keyboard input
    private void HandleJumpKeyboard()
    {

        if (Input.GetButtonDown("Jump") && !jumpPressed && playerMovement.isTouchingGround)
        {
            jumpPressed = true;
            playerMovement.jumpTimeRemained = playerMovement.maxJumpTime;
        }

        if (Input.GetButtonUp("Jump") || playerMovement.jumpTimeRemained < 0)
        {
            jumpPressed = false;
        }




    }

    //keyboard input
    private void HandleGravityChangeKeyboard()
    {
        if (Input.GetButtonDown("Jump") && !playerMovement.isTouchingGround)
        {
            playerMovement.attemtToChangeGravity = true;
        }


    }
}
