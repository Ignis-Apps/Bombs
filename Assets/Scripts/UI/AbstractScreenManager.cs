using Assets.Scripts.Game;
using UnityEngine;

public abstract class AbstractScreenManager : MonoBehaviour
{
    protected GameData gameData;
    protected GameManager gameManager;
    protected ScreenManager screenManager;

    public void Awake()
    {
        gameData = GameData.GetInstance();
        gameManager = GameManager.GetInstance();
        screenManager = ScreenManager.GetInstance();
        Init();
    }

    protected abstract void Init();
}
