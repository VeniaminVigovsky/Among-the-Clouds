using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrallaxEffect : MonoBehaviour
{
    //Camera
    public GameObject cam;
    //ChildObject of the Camera
    public Transform border;
    float spriteLength;
    //0 - closest to the player, visually stays at its place, 1 - moves entrilly along the camera, no going back
    [SerializeField] float parallaxEffect = 0.5f;
    SpriteRenderer spr;
    [SerializeField] float spriteOffset;
    
    Vector3 camPreviousPosition;

    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        spriteLength = spr.bounds.size.x;
        border.localPosition = new Vector3(-spriteLength + 1, transform.localPosition.y, transform.localPosition.z);
        transform.position = new Vector3(transform.position.x + spriteLength * spriteOffset, transform.position.y, transform.position.z);
        
        camPreviousPosition = cam.transform.position;
    }
    void FixedUpdate()
    {
        ApplyParallax();
    }

    void ApplyParallax()
    {
        //calculating what distance camera has passed, and applying parallax cooeffixcient to this value
        float distance = (cam.transform.position.x - camPreviousPosition.x) * parallaxEffect;
        ;
        //setting new transform.x value by adding the distance calculated in the previous step 
        transform.position = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);
        //check if the sprite has reached the border point (it's a child object of the camera)
        if (transform.position.x < border.position.x)
        {
            //Move sprite a few sprites to the right
            MoveSprite();
        }
        //storing the position of the camera so as to use it the next frame
        camPreviousPosition = cam.transform.position;
    }

    void MoveSprite()
    {
        //move the sprite 3 sprite length to the right
        transform.position = new Vector3(transform.position.x + spriteLength * 3, transform.position.y, transform.position.z);
    }

}
