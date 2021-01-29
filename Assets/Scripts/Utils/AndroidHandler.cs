using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidHandler : MonoBehaviour
{
    private GameStateManager gameMenuManager;
    private GameData gameData;
    private float previousTimeScale = 1f;
    void Start()
    {
        gameMenuManager = GameStateManager.GetInstance();
        gameData = GameData.GetInstance();
        
        gameData.LoadData();    // Passt bisher nirgends richtig hin
        Application.targetFrameRate = 300;
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause && gameMenuManager.CanPlayerMove())
        {
            Pause();
            gameData.SaveData();
        }
    }

    public void Pause()
    {
        gameMenuManager.SwitchController(GameMenu.PAUSE_SCREEN);
        previousTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    public void restore()
    {
        if (previousTimeScale > -1f)
        {
            Time.timeScale = previousTimeScale;
            previousTimeScale = -1f;
        }
    }
}
