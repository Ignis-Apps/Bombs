using System.Collections;
using System.Collections.Generic;
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

    private float scoreCountUpTime = .5f;
    private float scoreCountUpTimeProgress;

    private int targetScoreText;

    void Awake()
    {
        gameManager = GameManager.GetInstance();
        gameData = GameData.GetInstance();
       
    }

    private void Update()
    {
       if(scoreCountUpTimeProgress >= scoreCountUpTime) { return; }

        scoreCountUpTimeProgress += Time.unscaledDeltaTime;
        scoreCountUpTimeProgress = Mathf.Min(scoreCountUpTimeProgress, scoreCountUpTime);
        UpdateText();

    }

    private void OnEnable()
    {
        //scoreCountUpTimeProgress = scoreCountUpTime;
        targetScoreText = gameManager.Score;
        scoreCountUpTimeProgress = 0;
        //UpdateText();
    }


    private void UpdateText()
    {

        int score = (int)(scoreCountUpTimeProgress / scoreCountUpTime * targetScoreText);

        scoreText.SetText(score.ToString());
        survivedSecoundsText.SetText(gameManager.SurvivedSecounds.ToString());
        survivedWavesText.SetText(gameManager.SurvivedWaves.ToString());

        bestScoreText.SetText("Best " + gameData.HighScore);
        revivePriceText.SetText("50");

    }
}
