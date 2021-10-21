using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    [SerializeField] ObjectPooling[] objectsToPool;
    public Vector3 offsetPosition;
    public float y = 1f;
    private float tileSize = 0.25f;
    [SerializeField] ObjectPooling[] liveObjects;
    float lastHazardTime;
    float timeBetweenHazards = 3f;

    int liveCountBeforeLiveSpawning;

    private void Awake()
    {
        switch (Score.GetGameMode())
        {
            case Score.GameMode.Easy:
                timeBetweenHazards = 5f;
                liveCountBeforeLiveSpawning = 4;
                break;
            case Score.GameMode.Normal:
                timeBetweenHazards = 2f;
                liveCountBeforeLiveSpawning = 3;
                break;
            case Score.GameMode.Hard:
                timeBetweenHazards = 0.5f;
                liveCountBeforeLiveSpawning = 2;
                break;
            default:
                timeBetweenHazards = 2f;
                liveCountBeforeLiveSpawning = 3;
                break;
        }
    }

    public void GenerateObject(GameObject newPlatform)
    {
        int objectsToPoolArrayNumber = Random.Range(0, objectsToPool.Length);

        GameObject objectToPool = objectsToPool[objectsToPoolArrayNumber].GetPooledObject();

        if (objectToPool.CompareTag("Wires") && lastHazardTime + timeBetweenHazards > Time.time)
        {
            objectToPool.SetActive(false);
            return;
        }

        else if (objectToPool.CompareTag("Wires") && lastHazardTime + timeBetweenHazards < Time.time)
        {
            lastHazardTime = Time.time;
        }

        var x = UnityEngine.Random.Range(newPlatform.transform.position.x - newPlatform.GetComponent<BoxCollider2D>().size.x / 2 + tileSize, newPlatform.transform.position.x + newPlatform.GetComponent<BoxCollider2D>().size.x / 2 - tileSize);
        var position = new Vector3(x, newPlatform.transform.position.y, newPlatform.transform.position.z) + offsetPosition;
        objectToPool.transform.position = position;
        objectToPool.transform.rotation = transform.rotation;
        objectToPool.transform.localScale = new Vector3(1f, y, 1f);
        objectToPool.SetActive(true);


        if (objectsToPoolArrayNumber == 1 && PlayerDeath.liveCount <= liveCountBeforeLiveSpawning)
        {
            int randomPoolNumber = Random.Range(0, liveObjects.Length);

            GameObject liveObject = liveObjects[randomPoolNumber].GetPooledObject();
            liveObject.transform.position = position;
            liveObject.transform.rotation = transform.rotation;
            liveObject.transform.localScale = new Vector3(1f, y, 1f);
            liveObject.SetActive(true);
        }
    }
}
