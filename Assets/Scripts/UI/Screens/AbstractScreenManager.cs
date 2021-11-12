using Assets.Scripts.Game;
using UnityEngine;

public abstract class AbstractScreenManager : MonoBehaviour
{
    protected GameData gameData;
    protected GameManager gameManager;
    protected ScreenManager screenManager;

    protected FirebaseController firebaseController;
    protected AppodealController appodealController;
    protected GPGSController GpgsController;

    public void Awake()
    {
        gameData = GameData.GetInstance();
        gameManager = GameManager.GetInstance();
        screenManager = ScreenManager.GetInstance();

        firebaseController = FirebaseController.GetInstance();
        appodealController = AppodealController.GetInstance();
        GpgsController = GPGSController.GetInstance();

        Init();
    }

    protected abstract void Init();
}
