using Assets.Scripts.Game;
using UnityEngine;

public class GameLauncher : MonoBehaviour
{
    private GameData gameData;
    private FirebaseController firebaseController;
    private AppodealController appodealController;

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
        
        firebaseController.Init();
        appodealController.Init();

        Debug.Log("Finished initializing!");
    }

    private void InitIAP()
    {

    }

    
}
