using Assets.Scripts.Game;
using UnityEngine;

public class PauseDetector : MonoBehaviour
{
    private ScreenManager screenManager;
    private GameData gameData;
    private float previousTimeScale = 1f;
    void Start()
    {
        screenManager = ScreenManager.GetInstance();
        gameData = GameData.GetInstance();
        
        Application.targetFrameRate = 300;
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause && screenManager.CanPlayerMove())
        {
            Pause();
        }
    }

    public void Pause()
    {
        screenManager.SwitchScreen(ScreenType.PAUSE_SCREEN);
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
