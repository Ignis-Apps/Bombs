using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public static bool IsInStartMenu = true;
    public GameObject StartMenuUI;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        StartMenuUI.SetActive(false);
    }
}
