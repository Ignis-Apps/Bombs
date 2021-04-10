using Assets.Scripts.Game;
using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseDetector : MonoBehaviour
{
    private GameScreenManager gameScreenManager;
    private GameData gameData;
    private float previousTimeScale = 1f;
    void Start()
    {
        gameScreenManager = GameScreenManager.GetInstance();
        gameData = GameData.GetInstance();
        
        gameData.LoadData();    // Passt bisher nirgends richtig hin
        Application.targetFrameRate = 300;
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause && gameScreenManager.CanPlayerMove())
        {
            Pause();
            gameData.SaveData();
        }
    }

    public void Pause()
    {
        gameScreenManager.SwitchScreen(Assets.Scripts.UI.ScreenType.PAUSE_SCREEN);
        previousTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    public void Restore()
    {
        if (previousTimeScale > -1f)
        {
            Time.timeScale = previousTimeScale;
            previousTimeScale = -1f;
        }
    }
}
