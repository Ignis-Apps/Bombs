using Assets.Scripts.Game.Session;
using Assets.Scripts.UI.Screens.Tutorial;
using System.Collections;
using UnityEngine;

public class TutorialScreenManager : AbstractScreenManager
{   
    [SerializeField] private AbstractTutorial[] tutorials;   

    private IEnumerator tutorialCoroutine;
        
    protected override void Init()
    {
        gameObject.SetActive(false);
    }

    protected void OnEnable()
    {        
        GameSessionEventHandler.playerDiedDelegate += OnPlayerDied;
        gameManager.waveSpawner.enabled = false;        
        tutorialCoroutine = RunAllTutorials();
        StartCoroutine(tutorialCoroutine);
    }

    protected void OnDisable()
    {        
        GameSessionEventHandler.playerDiedDelegate -= OnPlayerDied;
        
        if(gameManager.waveSpawner!=null)
            gameManager.waveSpawner.enabled = true;
    }

    private void OnPlayerDied()
    {
        StopCoroutine(tutorialCoroutine);

        foreach (AbstractTutorial tutorial in tutorials)
        {
            tutorial.Dispose(); 
            tutorial.gameObject.SetActive(false);
        }
     
        
    }

    IEnumerator RunAllTutorials()
    {
        firebaseController.LogEvent(Firebase.Analytics.FirebaseAnalytics.EventTutorialBegin);

        foreach (AbstractTutorial tutorial in tutorials)
        {           
            yield return StartCoroutine(RunTutorial(tutorial));
        }
        yield return new WaitForSeconds(.5f);
        
        screenManager.SwitchScreen(ScreenType.INGAME_OVERLAY);
    }

  
    IEnumerator RunTutorial(AbstractTutorial tutorial)
    {
        Debug.Log("Tutorial Subroutin Start");
        tutorial.gameObject.SetActive(true);
        tutorial.Init();
        tutorial.StartTutorial();
                        
        while (!tutorial.IsTutorialComplete)
        {       
            yield return null;
        }

        tutorial.Dispose();
        tutorial.gameObject.SetActive(false);        

    }

}
