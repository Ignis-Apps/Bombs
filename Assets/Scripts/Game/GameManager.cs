using Assets.Scriptable;
using Assets.Scripts.Game;
using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private float daytime = 0;

    private float playerSpeedMultiplier = 1f;

    private int dodgedBombs;
    private int survivedWaves;

    public GameWave CurrentWave;
    public float CurrentWaveProgress;

    public bool PlayerHasShield;
    public bool IsPlayerMoving;
    public bool IsPlayerNearCrate;

    public GameObject Player { get; set; }
    public int CollectedCoins { get => collectedCoins; set { collectedCoins = value; } }
    public int CollectedPoints { get => collectedScorePoints; set { collectedScorePoints = value; } }
    public float PlayerSpeedFactor { get => playerSpeedMultiplier; set { playerSpeedMultiplier = value; } }
    public int SurvivedSecounds { get => (int)survivedSecounds; }
    public int SurvivedWaves { get => survivedWaves; }
    public float DayTime { get => CurrentWaveProgress + survivedWaves; }
    public int PlayerLifes { get => remainingLives; }

    public int Score { get => (1 + survivedWaves) * (int) survivedSecounds; }

    private GameUIMessageTypes currentMessage;

    public void OnCoinCollected(int amount){ collectedCoins += amount; }
    public void OnPointCollected(int amount){ collectedScorePoints += amount;}
    public void OnBombDodged(){ dodgedBombs += 1; }
    public void OnWaveSurvived(){ 
        survivedWaves++;
        getPlayer().OnWaveSurvived();
    }
    public void OnPlayerHit() { if (!PlayerHasShield) remainingLives--; }
    public void OnPlayerDied() {
        
        GameStateManager.GetInstance().SwitchController(Menu.GAME_OVER_SCREEN);
        Player.GetComponentsInChildren<SpriteRenderer>().ToList().ForEach(renderer => renderer.enabled = false);
        Player.GetComponent<MovementController>().enabled = false;
        GameData.GetInstance().HighScore = Score; }
    public void Tick(){ survivedSecounds += Time.deltaTime; }

    public void ResetStats()
    {
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
        //Player.SetActive(true);
        Player.GetComponentsInChildren<SpriteRenderer>().ToList().ForEach(renderer => renderer.enabled = true);
        Player.GetComponent<MovementController>().enabled = true;
        Player.transform.position = new Vector2(0, Player.transform.position.y);

        // Reset powerups
        Powerup.CurrentActivePowerup?.DeactivatePowerup();

        // Reset waves
        GameObject spawner = GameObject.FindGameObjectWithTag("Spawner");
        GameWaveSpawner waveSpawner = spawner.GetComponent<GameWaveSpawner>();
        waveSpawner.Reset();

        collectedCoins = 0;
        collectedScorePoints = 0;
        survivedSecounds = 0;
        dodgedBombs = 0;
        survivedWaves = 0;
        playerSpeedMultiplier = 1f;
        remainingLives = 3;

        IsPlayerNearCrate = false;
        IsPlayerMoving = false;

    }

    public void handleGameEvent(GameEvent gameEvent)
    {
        switch (gameEvent)
        {
            case GameEvent.RESET_GAME:
                GameData.GetInstance().SaveData();
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

    public Player getPlayer()
    {
        return Player.GetComponent<Player>();
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
