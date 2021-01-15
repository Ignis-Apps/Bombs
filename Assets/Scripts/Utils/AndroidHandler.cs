using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidHandler : MonoBehaviour
{
    private GameMenuManager gameMenuManager;
    private float previousTimeScale = 1f;
    void Start()
    {
        gameMenuManager = GameMenuManager.GetInstance();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause && gameMenuManager.CanPlayerMove())
        {
            gameMenuManager.SwitchController(GameMenu.PAUSE_SCREEN);
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }
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
