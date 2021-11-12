using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverScreenManager: AbstractScreenManager
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TextMeshProUGUI CoinsText;
    [SerializeField] TextMeshProUGUI survivedWavesText;
    
    [SerializeField] Button homeButton;
    [SerializeField] Button retryButton;
    [SerializeField] Button shareButton;
    private Animator animator;

    // Init (like Awake) when the script is initialized
    protected override void Init()
    {
        animator = GetComponent<Animator>();

        homeButton.onClick.AddListener(Home);
        retryButton.onClick.AddListener(Retry);
        shareButton.onClick.AddListener(Share);
    }

    private void OnEnable()
    {
        StartCoroutine(AnimateUI());

        // Provisorisch
        scoreText.SetText(CreateTimeString(gameManager.SurvivedSecounds));

        Firebase.Analytics.FirebaseAnalytics.LogEvent(Firebase.Analytics.FirebaseAnalytics.EventPostScore,
                new Firebase.Analytics.Parameter[] {
                    new Firebase.Analytics.Parameter(
                        Firebase.Analytics.FirebaseAnalytics.ParameterScore, gameManager.SurvivedSecounds)
                }
            );

        gameData.CoinBalance += gameManager.session.playerStats.Coins;

        Firebase.Analytics.FirebaseAnalytics.LogEvent(Firebase.Analytics.FirebaseAnalytics.EventEarnVirtualCurrency,
                new Firebase.Analytics.Parameter[] {
                    new Firebase.Analytics.Parameter(Firebase.Analytics.FirebaseAnalytics.ParameterValue, gameManager.session.playerStats.Coins),
                    new Firebase.Analytics.Parameter(Firebase.Analytics.FirebaseAnalytics.ParameterVirtualCurrencyName, "Coins"),
                }
            );

        gameData.CrystalBalance += gameManager.session.playerStats.Crystals;

        Firebase.Analytics.FirebaseAnalytics.LogEvent(Firebase.Analytics.FirebaseAnalytics.EventEarnVirtualCurrency,
                new Firebase.Analytics.Parameter[] {
                    new Firebase.Analytics.Parameter(Firebase.Analytics.FirebaseAnalytics.ParameterValue, gameManager.session.playerStats.Crystals),
                    new Firebase.Analytics.Parameter(Firebase.Analytics.FirebaseAnalytics.ParameterVirtualCurrencyName, "Crystals"),
                }
            );


        if (gameManager.SurvivedSecounds > gameData.HighScore) { 
            bestScoreText.SetText("New Highscore");
            gameData.HighScore = gameManager.SurvivedSecounds;

            Firebase.Analytics.FirebaseAnalytics.LogEvent("post_new_highscore", Firebase.Analytics.FirebaseAnalytics.ParameterScore, gameManager.SurvivedSecounds);
        } else
        {
            bestScoreText.SetText("Best " + CreateTimeString(gameData.HighScore));
        }

        // Set allways for the case, that the user sign in is after the install
        GpgsController.UpdateScoreTestLeaderboard(gameData.HighScore * 1000);
    }

    private string CreateTimeString(int seconds)
    {
        string output;
        int min = seconds / 60;
        int sec = seconds % 60;

        output = min + ":";
        if (sec < 10) { output += "0"; }
        output += sec;

        return output;
    }


    private void Reset()
    {
        scoreText.SetText("0");
        survivedWavesText.SetText("0");
        CoinsText.SetText("0");
    }

    private IEnumerator AnimateUI()
    {
        Reset();
        animator.Play("Transition_In");

        yield return new WaitForSeconds(.7f);

        // Sequenziell
        StartCoroutine(AnimateText(CoinsText, 0f, .25f, "", 0, gameManager.session.playerStats.Coins));
        yield return new WaitForSeconds(.5f);
        StartCoroutine(AnimateText(survivedWavesText, 0f, .5f, "", 0, gameManager.session.progressStats.SurvivedWaves));
        yield return new WaitForSeconds(.5f);
        //StartCoroutine(AnimateText(scoreText, 0f, .5f, "", 0, gameManager.SurvivedSecounds));
    }

    private IEnumerator AnimateText(TextMeshProUGUI target, float startDelay, float duration,string prefix, int startValue, int endValue)
    {   
        yield return new WaitForSeconds(startDelay);

        float elapsedTime = 0f;
        int animatedRange = endValue - startValue;

        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            elapsedTime = Mathf.Min(duration, elapsedTime);

            float progress = elapsedTime / duration;
            int value = (int) (progress * animatedRange + startValue);
            
            target.SetText(prefix + value.ToString());
            yield return null;
        }

    }

    private void Home()
    {
        gameManager.handleGameEvent(GameEvent.RESET_GAME);
        screenManager.SwitchScreen(ScreenType.TITLE_SCREEN);
    }

    private void Retry()
    {
        gameManager.handleGameEvent(GameEvent.RESET_GAME);
        if(gameData.TutorialWasPlayed)
            screenManager.SwitchScreen(ScreenType.INGAME_OVERLAY);
        else
            screenManager.SwitchScreen(ScreenType.TUTORIAL_SCREEN);
    }

    private void Share()
    {
        Debug.Log("Sharing ...");
    }
}
