using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    PlayerMovement playerMovement;
    public PlayerInput playerInput;
    PlayerAnimations playerAnimations;
    PlayerAudioManager audioManager;
    float originalSpeed;
    [SerializeField] Transform respawnPosition;
    [SerializeField] Transform respawnUpPosition;
    private float timeBeforeRestoreSpeed = 1f;
    Rigidbody2D rb;
    private float originalGravityScale;
    bool isDead;    
    private int maxLiveCount;
    public static int liveCount;
    [SerializeField] Transform platformBehindCheck;
    public LayerMask groundMask;
    private float distance = 3f;
    RaycastHit2D hit;
    bool isInvincible = false;

    SpriteRenderer playerSpriteRenderer;
    Material defaultMaterial;
    [SerializeField] Material flashMaterial;
    [SerializeField] float flashTime = 0.3f;

    public delegate void PlayerIsDead();
    public event PlayerIsDead PlayerDead;

    public static int sceneIndex;

    [SerializeField]
    PlatformGenerator platformGeneratorDOWN, platformGeneratorUP;

    private int hitDamage;
    private int coinValue;

    private void Awake()
    {
        switch (Score.GetGameMode())
        {
            case Score.GameMode.Easy:
                hitDamage = 3;
                coinValue = 5;
                break;
            case Score.GameMode.Normal:
                hitDamage = 5;
                coinValue = 5;
                break;
            case Score.GameMode.Hard:
                hitDamage = 7;
                coinValue = 3;
                break;
            default:
                hitDamage = 5;
                coinValue = 5;
                break;
        }
    }
    void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;        
        maxLiveCount = 5;
        liveCount = maxLiveCount;
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimations = GetComponent<PlayerAnimations>();
        
        rb = GetComponent<Rigidbody2D>();
        playerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        audioManager = GetComponent<PlayerAudioManager>();
        if (playerSpriteRenderer != null)
        {
            defaultMaterial = playerSpriteRenderer.material;
        }
        
    }

    private void Update()
    {
        

        if (isDead)
        {
            rb.velocity = new Vector2(0f, 0f);
            rb.gravityScale = 0f;
        }

        if(liveCount < 0)
        {
            liveCount = 0;
        }

        if (liveCount <= 0 && playerMovement.isGrounded())
        {
                        
            if (sceneIndex == 1)
            {
                
                Invoke("RestoreSpeed", timeBeforeRestoreSpeed);
                liveCount = maxLiveCount;
                rb.gravityScale = originalGravityScale;
                rb.isKinematic = false;
            }
            else
            {

                PlayerDied();
            }
        }
            
        if (hit.collider != null)
        {
            respawnPosition.position = new Vector3(hit.collider.gameObject.transform.position.x, respawnPosition.position.y, transform.position.z);
            respawnUpPosition.position = new Vector3(hit.collider.gameObject.transform.position.x, respawnUpPosition.position.y, transform.position.z);
        }
        
    }





    private void FixedUpdate()
    {
        hit = Physics2D.Raycast(platformBehindCheck.position, Vector2.right * -1, distance, groundMask);
        
        //respawnPosition.position = new Vector3(hit.collider.gameObject.transform.position.x, respawnPosition.position.y, transform.position.z);
        //respawnUpPosition.position = new Vector3(hit.collider.gameObject.transform.position.x, respawnUpPosition.position.y, transform.position.z);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            isDead = true;

            if (!isInvincible)
            {
                liveCount--;
                StopAllCoroutines();
                StartCoroutine(FlashOnHit());
                Score.score -= hitDamage;
                Score.canUpdateScore = false;
            }


            if (!playerMovement.upSideDown)
            {
                RespawnPlayer();
            }
            else if (playerMovement.upSideDown)
            {
                RespawnPlayerUp();
            }

            Invoke("RestoreSpeed", timeBeforeRestoreSpeed);

            isInvincible = true;
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            Score.score += coinValue;
            collision.gameObject.SetActive(false);
            audioManager.PlayCoinPickUpSound();
        }

        if (collision.gameObject.CompareTag("Wires"))
        {
            collision.gameObject.SetActive(false);
            
            if (!isInvincible)
            {
                Score.score -= hitDamage;
                StopAllCoroutines();
                StartCoroutine(FlashOnHit());
                liveCount--;
                isInvincible = true;
                audioManager.PlayHitSound();
                Invoke("BecomeDamagable", 1f);

            }
            

        }

        if (collision.gameObject.CompareTag("Live"))
        {
            audioManager.PlayLivePickUpSound();
            collision.gameObject.SetActive(false);
            liveCount++;
        }


    }

    private void RespawnPlayerUp()
    {
        playerAnimations.ChangeAnimation("Fall");
        playerInput.enabled = false;
        originalGravityScale = rb.gravityScale;

        if (platformGeneratorUP != null)
        {

            if (platformGeneratorUP.ActivePlatformsNumber() > 0)
            {
                transform.position = new Vector3(platformGeneratorUP.GetFirstPlatform().transform.position.x, respawnUpPosition.position.y, transform.position.z);
            }
            else if (platformGeneratorUP.ActivePlatformsNumber() <=0 && platformGeneratorDOWN != null && platformGeneratorDOWN.ActivePlatformsNumber() > 0)
            {
                playerMovement.gravityScaleMultiplier *= -1;
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1f, transform.localScale.z);
                playerMovement.upSideDown = !playerMovement.upSideDown;
                originalGravityScale *= -1;
                transform.position = new Vector3(platformGeneratorDOWN.GetFirstPlatform().transform.position.x, respawnPosition.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(respawnUpPosition.position.x, respawnUpPosition.position.y, transform.position.z);
            }
            
              
        }
        else
        {
            transform.position = new Vector3(respawnUpPosition.position.x, respawnUpPosition.position.y, transform.position.z);
        }
        if (playerMovement.movementSpeed > 1)
        {
            originalSpeed = playerMovement.movementSpeed;
        }


        isDead = true;
        playerMovement.movementSpeed = 0f;

        //playerMovement.upSideDown = wasUpSideDown;
    }

    private void RespawnPlayer()
    {
        playerAnimations.ChangeAnimation("Fall");
        playerInput.enabled = false;

        originalGravityScale = rb.gravityScale;


        if (platformGeneratorDOWN != null)
        {
            if (platformGeneratorDOWN.ActivePlatformsNumber() > 0)
            {
                transform.position = new Vector3(platformGeneratorDOWN.GetFirstPlatform().transform.position.x, respawnPosition.position.y, transform.position.z);
            }
            else if (platformGeneratorDOWN.ActivePlatformsNumber() <= 0 && platformGeneratorUP != null && platformGeneratorUP.ActivePlatformsNumber() > 0)
            {
                playerMovement.gravityScaleMultiplier *= -1;
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1f, transform.localScale.z);
                playerMovement.upSideDown = !playerMovement.upSideDown;
                
                originalGravityScale *= -1;

                transform.position = new Vector3(platformGeneratorUP.GetFirstPlatform().transform.position.x, respawnUpPosition.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(respawnPosition.position.x, respawnPosition.position.y, transform.position.z);
            }
        }
        else
        {
            transform.position = new Vector3(respawnPosition.position.x, respawnPosition.position.y, transform.position.z);
        }

        if (playerMovement.movementSpeed > 1)
        {
            originalSpeed = playerMovement.movementSpeed;
        }

        isDead = true;
        playerMovement.movementSpeed = 0f;


        //playerMovement.upSideDown = wasUpSideDown;

    }

    private void RestoreSpeed()
    {
        isDead = false;
        playerInput.enabled = true;
        playerMovement.movementSpeed = originalSpeed;
        playerMovement.jumpTimeRemained = -5f;
        rb.gravityScale = originalGravityScale;
        rb.isKinematic = false;
        Score.canUpdateScore = true;
        Invoke("BecomeDamagable", 1f);
    }

    private void PlayerDied()
    {

        if (playerMovement.movementSpeed > 0 )
        {
            playerMovement.movementSpeed -= Time.deltaTime;
        }
        rb.velocity = new Vector2(0f, 0f);

        playerInput.enabled = false;

        playerAnimations.ChangeAnimation("Destroy");
        audioManager.PlayDeathSound();

        Score.dead = true;

        Invoke("PlayerDeadEvent", 1f);
    }

    void PlayerDeadEvent()
    {
        PlayerDead?.Invoke();
    }


    void BecomeDamagable()
    {
        isInvincible = false;
    }

    IEnumerator FlashOnHit()
    {
        float t = Time.time;

        while (t > Time.time - timeBeforeRestoreSpeed)
        {
            if (flashMaterial != null)
                playerSpriteRenderer.material = flashMaterial;

            yield return new WaitForSeconds(flashTime);

            if (defaultMaterial != null)
                playerSpriteRenderer.material = defaultMaterial;

            yield return new WaitForSeconds(flashTime);
        }


    }
}
