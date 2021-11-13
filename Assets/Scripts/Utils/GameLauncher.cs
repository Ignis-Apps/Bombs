using Assets.Scripts.Game;
using UnityEngine;

public class GameLauncher : MonoBehaviour
{
    private GameData gameData;
    private FirebaseController firebaseController;
    private AppodealController appodealController;
    private GPGSController gpgsController;

    private void Awake()
    {
        Debug.Log("Loading Player Prefs...");

        Application.targetFrameRate = 300;

        // Load Game data from PlayerPref
        gameData = GameData.GetInstance();
        gameData.LoadData();

        Debug.Log("Finished Loading Player Prefs!");
    }

    private void Start()
    {
        // Initialize APIs (gameData is required for gdpr consent)

        Debug.Log("Init APIs (Firebase, Appodeal, GPGS) ...");
        firebaseController = FirebaseController.GetInstance();
        appodealController = AppodealController.GetInstance();
        gpgsController = GPGSController.GetInstance();

        firebaseController.Init();
        appodealController.Init();
        gpgsController.Init();
        Debug.Log("Finished Init APIs (Firebase, Appodeal, GPGS)!");
    }

}
