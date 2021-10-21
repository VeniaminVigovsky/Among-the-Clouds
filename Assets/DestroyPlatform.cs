using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlatform : MonoBehaviour
{

    private Transform destructionPoint;

    [SerializeField]
    private PlatformGenerator platformGenerator;

    public void SetPlatformGenerator(PlatformGenerator platformGenerator)
    {
        this.platformGenerator = platformGenerator;
    }

    void OnEnable()
    {
        platformGenerator?.AddPlatform(this.gameObject);
    }

    void OnDisable()
    {
        platformGenerator?.RemovePlatform(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject destructionPointObject = GameObject.Find("PlatformDestructionPoint");
        destructionPoint = destructionPointObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        DestroyPlatforms();
    }

    private void DestroyPlatforms()
    {
        if (destructionPoint != null)
        {
            if (destructionPoint.position.x > transform.position.x)
            {
                //Destroy(gameObject);
                gameObject.SetActive(false);
            }
        }
    }
}
