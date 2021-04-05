﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameUIUpdater : MonoBehaviour
{
    public TextMeshProUGUI CoinText;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI LiveText;

    private GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.GetInstance();
    }
    // TODO : Stop wasting ressources. Maybe we could use event based updates
    void Update()
    {
        CoinText.text = gameManager.CollectedCoins.ToString();
        ScoreText.text = gameManager.CollectedPoints.ToString();
        LiveText.text = gameManager.PlayerLifes.ToString();
    }
}
