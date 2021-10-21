using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInput playerInput;
    PlayerAnimations playerAnimations;
    Rigidbody2D rb;
    public bool upSideDown = false;
    public float gravityScaleMultiplier = 1f;
    public bool isJumping = false;
    public bool attemtToChangeGravity;
    [SerializeField] float jumpForce = 3f;    
    [SerializeField] Transform groundCheck = null;
    [SerializeField] LayerMask groundLayer = 0;
    [SerializeField] float distance = 1f;
    int lastTimeCheck = 0;
    public float jumpTimeRemained = -5f;
    public float maxJumpTime = 0.3f;
    private float fallMultiplier = 0.4f;
    public float movementSpeed = 2f;
    float amountToAddSpeed = 0.2f;
    float maxSpeed = 2.5f;
    float isGroundedBuffer;
    float coyouteTime = 0.1f;
    public bool isTouchingGround;
    [SerializeField] float initialSpeed = 1.9f;

    [SerializeField] private float airBorneMultiplier = 1.2f;
    [SerializeField] private float minYvelocity;
    [SerializeField] private float maxYvelocity;
    public static bool stopMovement = false;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] Transform canFlipCheck;

    PlayerAudioManager audioManager;
    private void Start()
    {
        movementSpeed = initialSpeed;
        PlayerDeath.liveCount = 5;
        stopMovement = false;
        Score.score = 0;
        Score.canUpdateScore = true;
        StartCoroutine(Score.ScoreUpdate());
        Score.dead = false;
        playerAnimations = GetComponent<PlayerAnimations>();
        rb = GetComponent<Rigidbody2D>();
        audioManager = GetComponent<PlayerAudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        jumpTimeRemained -= Time.deltaTime;
        isGroundedBuffer -= Time.deltaTime;
        
        IncreaseSpeed();
        VelocityClamp();
    }

    private void IncreaseSpeed()
    {
        int timePassed = Mathf.RoundToInt(Time.realtimeSinceStartup);

        if (PlayerDeath.liveCount > 0 & !stopMovement)
        {
            if (timePassed - lastTimeCheck > 30 & movementSpeed <= maxSpeed)
            {
                movementSpeed += amountToAddSpeed;
                lastTimeCheck = timePassed;
            }
        }
        
        else if (PlayerDeath.liveCount <= 0 && PlayerDeath.sceneIndex != 1)
        {
            stopMovement = true;
        }


    }

    private void FixedUpdate()
    {
        
        Move();        
        Jump();
        GroundBuffer();
        CanChangeGravity();
        ChangeGravityScale();
        
        
    }

    private void VelocityClamp()
    {
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, minYvelocity, maxYvelocity));
    }

    private void GroundBuffer()
    {
        if (isGrounded())
        {
            isGroundedBuffer = coyouteTime;
            
        }

        if (isGroundedBuffer >= 0)
        {
            isTouchingGround = true;
            isJumping = false;
        }
        else
        {
            isTouchingGround = false;
        }
    }

    private void Move()
    {
        if (!stopMovement)
        {
            if (isJumping)
            {
                rb.velocity = new Vector2(movementSpeed * airBorneMultiplier, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
            }


            if (isTouchingGround & !playerInput.jumpPressed)
            {
                playerAnimations.ChangeAnimation("Run");
            }
        }

        else if (stopMovement & isApproachingLedge())
        {
            movementSpeed = 0;
        }

    }

    void CanChangeGravity()
    {
        if (isTouchingGround || FlipCheck())
        {
            attemtToChangeGravity = false;
        }        
    }

    public void ChangeGravityScale()
    {
        if (attemtToChangeGravity)
        {
            attemtToChangeGravity = false;
            rb.gravityScale *= -1;
            gravityScaleMultiplier *= -1;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1f, transform.localScale.z);
            upSideDown = !upSideDown;
            playerAnimations.ChangeAnimation("Spin");
            audioManager.PlayGravityChangeSound();
        }
       
    }
        
    

    private void Jump()
    {
        if (playerInput.jumpPressed & isTouchingGround)
        {
            rb.velocity = new Vector2(rb.velocity.x * 1.2f, jumpForce * gravityScaleMultiplier);
            isJumping = true;
            playerAnimations.ChangeAnimation("Jump");
            audioManager.PlayJumpSound();
            isTouchingGround = false;
            isGroundedBuffer = -10f;
            
        }
        if (!upSideDown & !playerInput.jumpPressed & isJumping & rb.velocity.y >0)
        {
            rb.velocity = new Vector2(rb.velocity.x * 1.2f, rb.velocity.y * fallMultiplier * gravityScaleMultiplier);
            playerAnimations.ChangeAnimation("Fall");
        }
        else if(upSideDown & !playerInput.jumpPressed & isJumping & rb.velocity.y < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * 1.2f, rb.velocity.y * fallMultiplier * gravityScaleMultiplier);
            playerAnimations.ChangeAnimation("Fall");
        }
        
    }

    public bool isGrounded()
    {
        if (!upSideDown)
        {
            return Physics2D.Raycast(groundCheck.position, Vector2.down, distance, groundLayer);
        }
        else return Physics2D.Raycast(groundCheck.position, Vector2.up, distance, groundLayer);

    }

    public bool isApproachingLedge()
    {
        if (!upSideDown)
        {
            return Physics2D.Raycast(ledgeCheck.position, Vector2.down, distance, groundLayer);
        }
        else return Physics2D.Raycast(ledgeCheck.position, Vector2.up, distance, groundLayer);
    }

    public bool FlipCheck()
    {
        if (!upSideDown)
        {
            return Physics2D.Raycast(canFlipCheck.position, Vector2.down, distance, groundLayer);
        }
        else return Physics2D.Raycast(canFlipCheck.position, Vector2.up, distance, groundLayer);
    }
}
