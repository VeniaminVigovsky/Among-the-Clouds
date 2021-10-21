using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScroller : MonoBehaviour
{
    private float length;
    [SerializeField]
    private float offset;
    private float startingPosition;
    [SerializeField]
    private float startingOffset;
    void Start()
    {
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        startingPosition = transform.position.x;
        transform.position = new Vector3(transform.position.x + startingOffset, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x > startingPosition + (length + offset) * 2)
        {
            transform.position = new Vector3(startingPosition, transform.position.y, transform.position.z);
        }
    }
}
