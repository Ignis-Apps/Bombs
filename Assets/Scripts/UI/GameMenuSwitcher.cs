using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class GameMenuSwitcher : MonoBehaviour
{
    public GameMenu targetMenu;
    private GameStateManager stateManager;

    // Start is called before the first frame update
    void Start()
    {
        stateManager = GameStateManager.GetInstance();
        GetComponent<Button>().onClick.AddListener(OnButtonPressed);
    }

    void OnButtonPressed()
    {
        stateManager.SwitchController(targetMenu);
    }
}
