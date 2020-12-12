using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public static bool IsInStartMenu = true;
    public GameObject StartMenuUI;
    public GameObject IngameUI;

    public void Start()
    {
        StartMenuUI.SetActive(true);
        IngameUI.SetActive(false);
    }

    public void hide()
    {
        StartMenuUI.SetActive(false);
        IngameUI.SetActive(true);
        IsInStartMenu = false;

    }

    public void show()
    {
        StartMenuUI.SetActive(true);
        IngameUI.SetActive(false);
        IsInStartMenu = true;
    }
}
