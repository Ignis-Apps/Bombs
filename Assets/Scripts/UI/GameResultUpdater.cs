using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameResultUpdater : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TextMeshProUGUI survivedSecoundsText;
    [SerializeField] TextMeshProUGUI survivedWavesText;
    [SerializeField] TextMeshProUGUI revivePriceText;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.GetInstance();
    }

    private void Update()
    {
        UpdateText();
    }


    private void UpdateText()
    {

        scoreText.SetText(gameManager.GetPoints().ToString());
        survivedSecoundsText.SetText(gameManager.getSurvivedSecounds().ToString());
        survivedWavesText.SetText(gameManager.GetSurvivedWaves().ToString());

        bestScoreText.SetText("Best ?");
        revivePriceText.SetText("50");

    }
}
