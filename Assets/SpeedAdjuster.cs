using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedAdjuster : MonoBehaviour
{

    [SerializeField] PlayerMovement player;
    float playerSpeed;
    float speed;
    ParticleSystem ps;
   
    void Start()
    {
        ps = GetComponent<ParticleSystem>();

        speed = ps.startSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        playerSpeed = player.movementSpeed;
        ps.startSpeed = speed + playerSpeed;
    }
}
