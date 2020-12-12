using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{

    public GameObject bombPrefab;
    public GameObject coinPrefab;

    public Transform[] bombSpawnPoints;
    
    public float bombSpawnInterval;
    public float coinSpawnInterval;

    private float bombTimer;
    private float coinTimer;

    private bool running;

    public void StartDropping()
    {
        running = true;
    }

    public void StopDropping()
    {
        running = false;
    }

    public void Update()
    {
        if (!running)
            return;

        bombTimer += Time.deltaTime;
        coinTimer += Time.deltaTime;

        if(bombTimer > bombSpawnInterval)
        {
            bombTimer = 0f;
            SpawnBomb();
        }

        if (coinTimer > coinSpawnInterval)
        {
            coinTimer = 0f;
            SpawnCoin();
        }

    }

    

    void SpawnBomb()
    {
        Transform spawnPoint = bombSpawnPoints[Random.Range(0, bombSpawnPoints.Length)];
        Instantiate(bombPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    void SpawnCoin()
    {
        Transform spawnPoint = bombSpawnPoints[Random.Range(0, bombSpawnPoints.Length)];
        Instantiate(coinPrefab, spawnPoint.position, spawnPoint.rotation);
    }

 
}
