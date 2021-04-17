using Assets.Scriptable;
using Assets.Scripts.Game;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum GameEvent
{
    RESET_GAME,
    CHANGE_TIME,
    PAUSE_GAME,
    RESUME_GAME,
    REVIVE_PLAYER
}


public class GameManager : Singleton<GameManager>
{

    public PlayerStats playerStats = new PlayerStats();
    
    private float survivedSecounds;
    private float daytime = 0;

    private int dodgedBombs;
    private int survivedWaves;

    public GameWave CurrentWave;
    public float CurrentWaveProgress;

    public GameObject Player { get; set; }

    public int SurvivedSecounds { get => (int)survivedSecounds; }
    public int SurvivedWaves { get => survivedWaves; }
    public float DayTime { get => survivedSecounds / 45f; }
    
    public int Score { get => (1 + survivedWaves) * (int) survivedSecounds; }

    private GameUIMessageTypes currentMessage;

    public Transform GroundTransform { get; set; }

    public void OnCoinCollected(int amount){ playerStats.Coins += amount; }
    public void OnPointCollected(int amount){ playerStats.Score += amount;}

    public void OnCrystalCollected(int amount)
    {        
        playerStats.Crystals += amount;
    }
    public void OnBombDodged(){ dodgedBombs += 1; }
    public void OnWaveSurvived(){ 
        survivedWaves++;
        getPlayer().OnWaveSurvived();
    }
    public void OnPlayerHit() { if (!playerStats.IsProtected) playerStats.Lifes--; }
    public void OnPlayerDied() {
    
        if(playerStats.AmountOfRevives < 1)
        {
            ScreenManager.GetInstance().SwitchScreen(ScreenType.REVIVE_SCREEN);
            playerStats.IsProtected = true;
        }
        else
        {
            ScreenManager.GetInstance().SwitchScreen(ScreenType.GAME_OVER_SCREEN);
        }


        Player.GetComponentsInChildren<SpriteRenderer>().ToList().ForEach(renderer => renderer.enabled = false);        
        Player.GetComponent<MovementController>().Stop();

    }
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

        survivedSecounds = 0;
        dodgedBombs = 0;
        survivedWaves = 0;

        Time.timeScale = 1f;        

        playerStats.Reset();

    }

    private float previousTimeScale = 1f;

    public void OnGamePaused()
    {
       previousTimeScale = Time.timeScale;
       Time.timeScale = 0f;
    }

    public void OnGameResumed()
    {
      
        if (previousTimeScale > -1f)
        {
            Time.timeScale = previousTimeScale;
            Time.timeScale = 1f;
            previousTimeScale = -1f;
        }
        
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

            case GameEvent.PAUSE_GAME:
                OnGamePaused();
                break;

            case GameEvent.RESUME_GAME:
                OnGameResumed();
                break;

            case GameEvent.REVIVE_PLAYER:
                playerStats.Lifes += 1;
                playerStats.AmountOfRevives += 1;
                playerStats.IsProtected = false;
                Player.GetComponentsInChildren<SpriteRenderer>().ToList().ForEach(renderer => renderer.enabled = true);
                Player.GetComponent<MovementController>().enabled = true;
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
