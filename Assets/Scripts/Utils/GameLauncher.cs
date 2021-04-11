using Assets.Scripts.Game;
using UnityEngine;

public class GameLauncher : MonoBehaviour
{
    private GameData gameData;
    private FirebaseController firebaseController;
    private AppodealController appodealController;
    private GPGSController gpgsController;

    void Start()
    {
        Debug.Log("Initializing...");

        Application.targetFrameRate = 300;

        // Load Game data from PlayerPref
        gameData = GameData.GetInstance();
        gameData.LoadData();

        // Initialize APIs (gameData is required for gdpr consent)
        firebaseController = FirebaseController.GetInstance();
        appodealController = AppodealController.GetInstance();
        gpgsController = GPGSController.GetInstance();

        firebaseController.Init();
        appodealController.Init();
        gpgsController.Init();

        Debug.Log("Finished initializing!");
    }

}
