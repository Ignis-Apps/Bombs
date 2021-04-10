using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ScreenSwitcher : MonoBehaviour
{
    public Assets.Scripts.UI.ScreenType targetMenu;
    private GameScreenManager stateManager;

    // Start is called before the first frame update
    void Start()
    {
        stateManager = GameScreenManager.GetInstance();
        GetComponent<Button>().onClick.AddListener(OnButtonPressed);
    }

    void OnButtonPressed()
    {
        stateManager.SwitchScreen(targetMenu);
    }

    private void Update()
    {
        
    }
}
