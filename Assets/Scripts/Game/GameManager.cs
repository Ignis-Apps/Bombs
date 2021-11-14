using Assets.Scriptable;
using Assets.Scripts.Game;
using Assets.Scripts.Game.Session;
using System.Collections.Generic;
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

    public GameSession session = new GameSession();    
    
    public GameManager()
    {
        GameSessionEventHandler.playerDiedDelegate += OnPlayerDied;
    }

    ~GameManager()
    {
        GameSessionEventHandler.playerDiedDelegate -= OnPlayerDied;
    }

    public GameWave CurrentWave;

    public GameWaveSpawner waveSpawner;
    public GameObject PlayerObject { get; set; }
    public Transform GroundTransform { get; set; }

    public int SurvivedSecounds { get => (int) session.progressStats.SecoundsSurvived; }
    public float DayTime { get => session.progressStats.SecoundsSurvived / 45f; }
    
    private GameUIMessageTypes currentMessage;

    public void OnPlayerDied() {
    
        if(session.playerStats.AmountOfRevives < 1)
        {
            ScreenManager.GetInstance().SwitchScreen(ScreenType.REVIVE_SCREEN);
            session.playerStats.IsProtected = true;
        }
        else
        {
            ScreenManager.GetInstance().SwitchScreen(ScreenType.GAME_OVER_SCREEN);
        }

        Powerup.CurrentActivePowerup?.DeactivatePowerup();

    }
    public void Tick(){ session.progressStats.SecoundsSurvived += Time.deltaTime; }

    public void ResetStats()
    {
        // Delete all old objects
        List<GameObject> deleteList = new List<GameObject>();
        deleteList.AddRange(GameObject.FindGameObjectsWithTag("Coin"));
        deleteList.AddRange(GameObject.FindGameObjectsWithTag("ScoreOrb"));
        deleteList.AddRange(GameObject.FindGameObjectsWithTag("Bomb"));
        deleteList.AddRange(GameObject.FindGameObjectsWithTag("Crate"));
        deleteList.AddRange(GameObject.FindGameObjectsWithTag("Powerup"));
        foreach (GameObject o in deleteList) { Destroy(o); }

        // Delete ingame message
        currentMessage = GameUIMessageTypes.NONE;
  
        // Reset powerups
        Powerup.CurrentActivePowerup?.DeactivatePowerup();

        // Reset waves
        GameObject spawner = GameObject.FindGameObjectWithTag("Spawner");
        GameWaveSpawner waveSpawner = spawner.GetComponent<GameWaveSpawner>();
        waveSpawner.Reset();
       
        Time.timeScale = 1f;        
       
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
                GameSessionEventHandler.sessionResetDelegate();
                ResetStats();
                break;

            case GameEvent.CHANGE_TIME:
              //  daytime += 0.25f;
                //daytime %= 1f;
                break;

            case GameEvent.PAUSE_GAME:
                OnGamePaused();
                break;

            case GameEvent.RESUME_GAME:
                OnGameResumed();
                break;

            case GameEvent.REVIVE_PLAYER:                             
                GameSessionEventHandler.playerRevivedDelegate();              
                break;

            default:
                break;
        }
    }

    public Player getPlayer()
    {
        if(PlayerObject == null)
        {
            Debug.LogError("Player IS NULL");
        }

        return PlayerObject.GetComponent<Player>();
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
