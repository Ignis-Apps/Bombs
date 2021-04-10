using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScreenManger : MonoBehaviour
{
    private ScreenManager screenManager;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.GetInstance();
        screenManager = ScreenManager.GetInstance();
    }
}
