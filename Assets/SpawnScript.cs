using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{

    public GameObject bombPrefab;
    public Transform[] spawnPoints;
    private float spawnInterval = 2.5f;

    public void startDropping()
    {
        InvokeRepeating("spawnBomb", 0f, spawnInterval);
    }

    public void stopDropping()
    { 
        CancelInvoke();
    }
 
    void spawnBomb()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(bombPrefab, spawnPoint.position, spawnPoint.rotation);
    }

 
}
