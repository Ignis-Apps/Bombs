using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameEvent
{
    RESET_GAME,
    CHANGE_TIME
}


public class GameManager : Singleton<GameManager>
{
    public bool IsGameRunning;

    private int collectedCoins;
    private int collectedScorePoints;
    private int remainingLives = 3;
    
    private float survivedSecounds;
    private float daytime =0.1f;
    private float playerSpeedMultiplier = 1f;

    private int dodgedBombs;
    private int survivedWaves;

    public int CollectedCoins { get => collectedCoins; set { collectedCoins = value; } }
    public int CollectedPoints { get => collectedScorePoints; set { collectedScorePoints = value; } }
    public float PlayerSpeedFactor { get => playerSpeedMultiplier; set { playerSpeedMultiplier = value; } }
    public int SurvivedSecounds { get => (int)survivedSecounds; }
    public int SurvivedWaves { get => survivedWaves; }
    public float DayTime { get => daytime; }
    public int PlayerLifes { get => remainingLives; }

    public int Score { get => (1 + survivedWaves) * (int) survivedSecounds; }

    private GameUIMessageTypes currentMessage;

    public void OnCoinCollected(int amount){ collectedCoins += amount; }
    public void OnPointCollected(int amount){ collectedScorePoints += amount;}
    public void OnBombDodged(){ dodgedBombs += 1; }
    public void OnWaveSurvived(){ survivedWaves++; }
    public void OnPlayerHit() { remainingLives--; }
    public void OnPlayerDied() { GameData.GetInstance().HighScore = Score; }
    public void Tick(){ survivedSecounds += Time.deltaTime; }

    public void ResetStats()
    {
        collectedCoins = 0;
        collectedScorePoints = 0;
        survivedSecounds = 0;
        dodgedBombs = 0;
        survivedWaves = 0;
        playerSpeedMultiplier = 1f;
        remainingLives = 3;

        // Delete all old objects
        List<GameObject> deleteList = new List<GameObject>();
        deleteList.AddRange(GameObject.FindGameObjectsWithTag("Coin"));
        deleteList.AddRange(GameObject.FindGameObjectsWithTag("ScoreOrb"));
        deleteList.AddRange(GameObject.FindGameObjectsWithTag("Bomb"));
        deleteList.AddRange(GameObject.FindGameObjectsWithTag("Crate"));
        foreach (GameObject o in deleteList) { Destroy(o); }

        // Delete ingame message
        currentMessage = GameUIMessageTypes.NONE;

        // Reset player position
        GameObject player = getPlayer();
        player.transform.position = new Vector2(0, player.transform.position.y);

        // Reset waves
        GameObject spawner = GameObject.FindGameObjectWithTag("Spawner");
        GameWaveSpawner waveSpawner = spawner.GetComponent<GameWaveSpawner>();
        waveSpawner.Reset();


    }

    public void handleGameEvent(GameEvent gameEvent)
    {
        switch (gameEvent)
        {
            case GameEvent.RESET_GAME:
                ResetStats();
                break;

            case GameEvent.CHANGE_TIME:
                daytime += 0.25f;
                daytime %= 1f;
                break;

            default:
                break;
        }
    }

    public GameObject getPlayer()
    {
        return GameObject.FindGameObjectWithTag("Player");
    }    

    public void SetCurrentGameMessage(GameUIMessageTypes message)
    {
        currentMessage = message;
    }

    public GameUIMessageTypes getCurrentMessage()
    {
        GameUIMessageTypes output = currentMessage;
        currentMessage = GameUIMessageTypes.NONE;
        return output;
    }


}
