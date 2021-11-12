using Assets.Scripts.Game;
using Assets.Scripts.UI.Screens.Tutorial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialComplete : AbstractTutorial
{
    [SerializeField] private Button TutorialDoneButton;

    public override void Dispose(){}

    public override void Init()
    {
        TutorialDoneButton.onClick.AddListener(CompleteTutorial);
    }

    protected override void OnTutorialComplete()
    {
        GameData gameData = GameData.GetInstance();
        gameData.TutorialWasPlayed = true;
        gameData.SaveData();

        firebaseController.LogEvent(Firebase.Analytics.FirebaseAnalytics.EventTutorialComplete);
    }

    protected override void OnTutorialStart()
    {
        
    }
}
