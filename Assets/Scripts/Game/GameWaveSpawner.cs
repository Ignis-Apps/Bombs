using Assets.Scriptable;
using Assets.Scripts.Game.Session;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.Game
{
    //[ExecuteInEditMode]
    public class GameWaveSpawner : MonoBehaviour
    {
        [Header("Spawn Field Settings")]
        [SerializeField] private float spawnFieldWidth;
        [SerializeField] private int spawnColums;
        [SerializeField] private float spawnRowOffset;      
        [SerializeField] private float screenEdgeMargin;
        [SerializeField] private float verticalSpawnSalt;
        [SerializeField] private Transform LeftBorder;
        [SerializeField] private Transform RightBorder;

        [Header("Dev/Test Settings")]
        [SerializeField] private int debugSpawnBombAtPosition;
        [SerializeField] private bool repeatWave;

        [Header("Spawn Wave Settings")]
        [SerializeField] private GameWaveSettings gameWaveSettings;

        private Vector3[] spawnPoints;

        private ScreenManager screenManager = null;
        private GameManager gameManager = null;


        private GameWave currentWave;
        private int currentWaveIndex;
        private bool running;
        private bool onTimeout;

        private GameTimer waveTimer;
        private GameTimer spawnTimer;
        private GameTimer crateTimer;

        [SerializeField] private float crateDropTime;        

        
        [Header("Spawnable Gameobjects")]
        [SerializeField] private GameObject defaultBomb;
        [SerializeField] private GameObject homingRocket;
        [SerializeField] private GameObject smallBomb;
        [SerializeField] private GameObject crate;

        public void Awake()
        {   
            screenManager = ScreenManager.GetInstance();
            gameManager = GameManager.GetInstance();

            gameManager.waveSpawner = this;

            waveTimer = new GameTimer();
            spawnTimer = new GameTimer();
            
            crateTimer = new GameTimer();
            crateTimer.SetTargetTime(crateDropTime);

            CreateSpawnPoints();
            LoadWave(0);
        }

  
        public void Update()
        {
            
            if (screenManager == null || gameManager == null)
                return;
            
            running = screenManager.CanPlayerMove();


            if (running)            
                gameManager.Tick();
            

            gameManager.session.progressStats.currentWaveProgress = waveTimer.getProgress();
            
            if (!running || onTimeout) 
                return;

            waveTimer.Tick(Time.deltaTime);
            spawnTimer.Tick(Time.deltaTime);
            crateTimer.Tick(Time.deltaTime);
            

            // Load next wave
            if (waveTimer.IsDone() || waveTimer.GetRemainingTime() < spawnTimer.GetRemainingTime())
            {
                Firebase.Analytics.FirebaseAnalytics.LogEvent(Firebase.Analytics.FirebaseAnalytics.EventLevelEnd,
                new Firebase.Analytics.Parameter[] {
                    new Firebase.Analytics.Parameter(
                        Firebase.Analytics.FirebaseAnalytics.ParameterLevelName, "wave_" + currentWaveIndex)}
                );

                int nextWave = Mathf.Min(currentWaveIndex + 1, gameWaveSettings.waves.Length - 1);

                if (repeatWave){ nextWave = currentWaveIndex;}

                StartCoroutine(LoadWaveDelayed(2f,nextWave));
                if (currentWave.SpawnCrateAtEnd())
                {
                    // SpawnCrate();
                }
                
                return;
            }

            // Spawn bombs
            if (spawnTimer.IsDone())
            {
                // Decrease spawn time
                spawnTimer.SetTargetTime(gameWaveSettings.waves[currentWaveIndex].GetSpawnInterval(waveTimer.getProgress()));                
                SpawnBombs();
                spawnTimer.Reset();                
            }

            if (crateTimer.IsDone())
            {
                SpawnCrate();                
                crateTimer.Reset();
            }
           

        }

        IEnumerator LoadWaveDelayed(float time, int wave)
        {
            onTimeout = true;
            yield return new WaitForSeconds(1.5f);                        
            GameSessionEventHandler.waveCompleteDelegate();
            LoadWave(wave);
            yield return new WaitForSeconds(4f);
            onTimeout = false;
        }



        private void SpawnBombs()
        {

            int spawnAmount = currentWave.GetSpawnGroupSize();
            //Debug.Log("Spawning " + spawnAmount + "bombs ");
            List<int> spawnPositions = CreateUniqueNumbers(0, spawnColums, spawnAmount);

            foreach (int position in spawnPositions)
            {
                Vector3 spawnPosition = GetSpawnPoint(position);
                spawnPosition.y += Random.Range(0, verticalSpawnSalt);
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

                    case GameWave.SpawnType.SMALL_BOMB:
                        spawnedObject = Instantiate(smallBomb);
                        break;

                    default:
                        continue;
                }

                spawnedObject.transform.position = spawnPosition;

            }

        }

        public void SpawnBombAtRandomPosititon()
        {
            Vector3 spawnPosition = GetSpawnPoint(Random.Range(0, spawnColums));
            GameObject spawnedObject = Instantiate(defaultBomb);
            spawnedObject.transform.position = spawnPosition;
        }

        private void SpawnCrate()
        {
            int positionIndex = Random.Range(0, spawnColums);
            Vector3 spawnPosition = GetSpawnPoint(positionIndex);
            GameObject c = Instantiate(crate);
            c.transform.position = spawnPosition;

        }

        public void Reset()
        {
            LoadWave(0);
        }

        private void LoadWave(int waveIndex)
        {

            if(gameManager == null) { return; }

            Firebase.Analytics.FirebaseAnalytics.LogEvent(Firebase.Analytics.FirebaseAnalytics.EventLevelStart,
                new Firebase.Analytics.Parameter[] {
                    new Firebase.Analytics.Parameter(
                        Firebase.Analytics.FirebaseAnalytics.ParameterLevelName, "wave_" + waveIndex)}
                );

            if (waveIndex > 0)
            {
                gameManager.SetCurrentGameMessage(GameUIMessageTypes.WAVE_COMPLETE);
            }
            else
            {
                crateTimer.Reset();
            }

            //Debug.Log("LOADING NEXT WAVE");
            GameWave wave = gameWaveSettings.waves[waveIndex];
            currentWaveIndex = waveIndex;
            currentWave = wave;
            
            gameManager.CurrentWave = wave;
            
            ResetTimers();

            waveTimer.SetTargetTime(wave.GetWaveDuration());
            spawnTimer.SetTargetTime(wave.GetSpawnInterval(0f));

            gameManager.session.playerStats.SpeedWave = wave.GetPlayerSpeedMultiplier();

        }

        private void ResetTimers()
        {
            waveTimer.Reset();
            spawnTimer.Reset();            
        }

        private void CreateSpawnPoints()
        {
            spawnPoints = new Vector3[spawnColums];

            float startX = ((spawnFieldWidth / 2f)) + screenEdgeMargin;
            float endX = ((-spawnFieldWidth / 2f)) - screenEdgeMargin;
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
            Vector3 spawnPointOffset = new Vector3(spawnPoint.x, spawnPoint.y, spawnPoint.z);
            spawnPointOffset.y += spawnHeight;
            spawnPointOffset.x += transform.position.x;

            if (spawnPointOffset.x > RightBorder.transform.position.x)
            {
                spawnPointOffset.x -= spawnFieldWidth;
            }

            if(spawnPointOffset.x < LeftBorder.transform.position.x)
            {
                spawnPointOffset.x += spawnFieldWidth;
            }


            return spawnPointOffset;
        }


    }
}
