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

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.GetInstance();
        gameData = GameData.GetInstance();
       
    }

    private void Update()
    {
        UpdateText();
    }

    private void UpdateText()
    {

        scoreText.SetText(gameManager.Score.ToString());
        survivedSecoundsText.SetText(gameManager.SurvivedSecounds.ToString());
        survivedWavesText.SetText(gameManager.SurvivedWaves.ToString());

        bestScoreText.SetText("Best " + gameData.HighScore);
        revivePriceText.SetText("50");

    }
}
