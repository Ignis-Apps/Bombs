using System.Collections;
using UnityEngine;
using TMPro;
using Assets.Scripts.Game;
using UnityEngine.UI;

public class GameOverScreenManager: AbstractScreenManager
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TextMeshProUGUI survivedSecoundsText;
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
        bestScoreText.SetText("Best " + gameData.HighScore);
        if (gameManager.SurvivedSecounds > gameData.HighScore) { 
            bestScoreText.SetText("New Highscore");
            gpgsController.UpdateScoreTestLeaderboard(gameData.HighScore * 1000);
        }        
    }


    private void Reset()
    {
        scoreText.SetText("?");
        survivedWavesText.SetText("?");
        survivedSecoundsText.SetText("???");
    }

    private IEnumerator AnimateUI()
    {
        Reset();
        animator.Play("Transition_In");

        yield return new WaitForSeconds(.7f);

        // Sequenziell
        StartCoroutine(AnimateText(survivedSecoundsText, 0f, .25f, "", 0, gameManager.SurvivedSecounds));
        yield return new WaitForSeconds(.5f);
        StartCoroutine(AnimateText(survivedWavesText, 0f, .5f, "", 0, gameManager.SurvivedWaves));
        yield return new WaitForSeconds(.5f);
        StartCoroutine(AnimateText(scoreText, 0f, .5f, "", 0, gameManager.Score));
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
        screenManager.SwitchScreen(ScreenType.TITLE_SCREEN);
        gameManager.handleGameEvent(GameEvent.RESET_GAME);
    }

    private void Retry()
    {
        screenManager.SwitchScreen(ScreenType.INGAME_OVERLAY);
        gameManager.handleGameEvent(GameEvent.RESET_GAME);
    }

    private void Share()
    {
        Debug.Log("Sharing ...");
    }
}
