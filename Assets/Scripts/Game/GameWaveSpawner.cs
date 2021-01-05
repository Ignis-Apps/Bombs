using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.Game
{
    public class GameWaveSpawner : MonoBehaviour
    {
        [SerializeField]
        private float spawnFieldWidth;
        [SerializeField]
        private int spawnColums;
        [SerializeField]
        private float spawnRowOffset;
        [SerializeField]
        private float screenEdgeMargin;

        private Vector3[] spawnPoints;

        private GameMenuManager gameMenuManager;
        private GameManager gameManager;

        [SerializeField]
        private GameWave[] gameWaves;
        private GameWave currentWave;
        private int currentWaveIndex;
        private bool running;

        private GameTimer waveTimer;
        private GameTimer spawnTimer;

        // Spawnable game objects
        [SerializeField]
        private GameObject defaultBomb;
        [SerializeField]
        private GameObject homingRocket;

        public void Start()
        {
            gameMenuManager = GameMenuManager.GetInstance();
            gameManager = GameManager.GetInstance();

            waveTimer = new GameTimer();
            spawnTimer = new GameTimer();
            CreateSpawnPoints();
            LoadWave(0);
        }

        public void Update()
        {
            running = gameMenuManager.CanPlayerMove();
            if (!running) return;

            waveTimer.Tick(Time.deltaTime);
            spawnTimer.Tick(Time.deltaTime);

            // Load next wave
            if (waveTimer.IsDone())
            {
                int nextWave = Mathf.Min(currentWaveIndex + 1, gameWaves.Length - 1);
                LoadWave(nextWave);
                return;
            }

            // Spawn bombs
            if (spawnTimer.IsDone())
            {
                // Decrease spawn time
                spawnTimer.SetTargetTime(gameWaves[currentWaveIndex].GetSpawnInterval(waveTimer.getProgress()));
                SpawnBombs();
                spawnTimer.Reset();
                Debug.Log("SPAWNING !!!");
            }

        }

        private void SpawnBombs()
        {

            int spawnAmount = currentWave.GetSpawnGroupSize();
            Debug.Log("Spawning " + spawnAmount + "bombs ");
            List<int> spawnPositions = CreateUniqueNumbers(0, spawnColums, spawnAmount);

            foreach (int position in spawnPositions)
            {
                Vector3 spawnPosition = GetSpawnPoint(position);
                GameWave.SpawnType spawnType = currentWave.GetRandomSpawnType();
                GameObject spawnedObject;
                switch (spawnType)
                {
                    case GameWave.SpawnType.DEFAULT_BOMB:
                        spawnedObject = Instantiate(defaultBomb);
                        break;

                    case GameWave.SpawnType.HOMING_BOMB:
                        spawnedObject = Instantiate(homingRocket);
                        break;
                    default:
                        continue;
                }

                spawnedObject.transform.position = spawnPosition;

            }

        }

        public void Reset()
        {
            LoadWave(0);
        }

        private void LoadWave(int waveIndex)
        {

            Debug.Log("LOADING NEXT WAVE");
            GameWave wave = gameWaves[waveIndex];
            currentWaveIndex = waveIndex;
            currentWave = wave;
            ResetTimers();

            waveTimer.SetTargetTime(wave.GetWaveDuration());
            spawnTimer.SetTargetTime(wave.GetSpawnInterval(0f));

            gameManager.setPlayerSpeedMultiplier(wave.GetPlayerSpeedMultiplier());

        }

        private void ResetTimers()
        {
            waveTimer.Reset();
            spawnTimer.Reset();
        }

        private void CreateSpawnPoints()
        {
            spawnPoints = new Vector3[spawnColums];

            float startX = (transform.position.x - (spawnFieldWidth / 2f)) + screenEdgeMargin;
            float endX = (transform.position.x + (spawnFieldWidth / 2f)) - screenEdgeMargin;
            float spacing = ((endX - startX) / (spawnColums-1));

            for (int i = 0; i < spawnPoints.Length; i++)
            {
                spawnPoints[i] = new Vector3(startX + (i * spacing), transform.position.y, transform.position.z);
            }

        }

        private List<int> CreateUniqueNumbers(int from, int to, int amount)
        {

            List<int> output = new List<int>();
            for (int i = from; i < to; i++)
            {
                output.Add(i);
            }

            for (int i = output.Count - amount; i > 0; i--)
            {
                output.RemoveAt(Random.Range(0, output.Count));
            }
            return output;


        }

        private Vector3 GetSpawnPoint(int position)
        {
            int spawnPosition = position % spawnColums;
            float spawnHeight = (position / spawnColums) * spawnRowOffset;

            Vector3 spawnPoint = spawnPoints[spawnPosition];
            spawnPoint.y += spawnHeight;

            return spawnPoint;
        }


    }
}
