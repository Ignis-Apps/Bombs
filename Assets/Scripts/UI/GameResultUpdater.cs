using System.Collections;
using UnityEngine;
using TMPro;
using Assets.Scripts.Game;

public class GameResultUpdater : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TextMeshProUGUI survivedSecoundsText;
    [SerializeField] TextMeshProUGUI survivedWavesText;
    [SerializeField] TextMeshProUGUI revivePriceText;

    private GameManager gameManager;
    private GameData gameData;
    private Animator animator;

    void Awake()
    {
        gameManager = GameManager.GetInstance();
        gameData = GameData.GetInstance();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        StartCoroutine(AnimateUI());
        bestScoreText.SetText("Best " + gameData.HighScore);
        revivePriceText.SetText("50");
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
}
