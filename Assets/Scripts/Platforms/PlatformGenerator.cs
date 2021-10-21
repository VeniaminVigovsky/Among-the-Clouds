using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField] GameObject platformPrefab = null;
    [SerializeField] Transform generationPoint = null;

    ObjectGenerator objectGenerator;
    
    [SerializeField] float distanceBetween = 1f;
    [SerializeField] float minDistance = 0.5f;
    [SerializeField] float maxDistance = 1.75f;
    private float platformWidth;
    float timeStarted;

    public ObjectPooling[] objectPoolings;

    public ObjectPooling objectPooler;

    public bool generatePlatform = true;

    public delegate void PlatfromGeneratedHandler();
    public event PlatfromGeneratedHandler PlatformGenerated;


    private List<GameObject> activePlatforms = new List<GameObject>();

    void Start()
    {
        objectGenerator = GetComponent<ObjectGenerator>();

        timeStarted = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        GeneratePlatforms();



        if (Time.time - timeStarted > 30f)
        {
            maxDistance = 1.75f;
        }

        else if (Time.time - timeStarted > 60f)
        {
            maxDistance = 2.25f;
        }

        else if (Time.time - timeStarted > 90f)
        {
            maxDistance = 3.5f;
        }

        else maxDistance = 1.25f;

       
    }



    private void GeneratePlatforms()
    {
        if (generationPoint != null)
        {
            if (transform.position.x < generationPoint.position.x)
            {
                if (generatePlatform)
                {
                    distanceBetween = UnityEngine.Random.Range(minDistance, maxDistance);
                    int objectPoolingArrayNumber = UnityEngine.Random.Range(0, objectPoolings.Length);
                    platformWidth = objectPoolings[objectPoolingArrayNumber].pooledObject.GetComponent<BoxCollider2D>().size.x;

                    transform.position = new Vector3(transform.position.x + distanceBetween + (platformWidth / 2), transform.position.y, transform.position.z);



                    GameObject newPlatform = objectPoolings[objectPoolingArrayNumber].GetPooledObject();

                    newPlatform.transform.position = transform.position;
                    newPlatform.transform.rotation = transform.rotation;

                    newPlatform.GetComponent<DestroyPlatform>().SetPlatformGenerator(this);

                    newPlatform.SetActive(true);

                    PlatformGenerated?.Invoke();

                    objectGenerator?.GenerateObject(newPlatform);
                }
                else
                {
                    distanceBetween = UnityEngine.Random.Range(minDistance, maxDistance);
                    int objectPoolingArrayNumber = UnityEngine.Random.Range(0, objectPoolings.Length);
                    platformWidth = objectPoolings[objectPoolingArrayNumber].pooledObject.GetComponent<BoxCollider2D>().size.x;

                    transform.position = new Vector3(transform.position.x + distanceBetween + (platformWidth / 2), transform.position.y, transform.position.z);
                }




            }
        }
    }

    public void AddPlatform(GameObject platform)
    {
        activePlatforms.Add(platform);
    }

    public void RemovePlatform(GameObject platform)
    {
        activePlatforms.Remove(platform);
    }

    public GameObject GetFirstPlatform()
    {
        return activePlatforms.Count != 0 ? activePlatforms[0] : null;
    }

    public int ActivePlatformsNumber()
    {
        return activePlatforms.Count;
    }
}
