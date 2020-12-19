using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class GameMenuSwitcher : MonoBehaviour
{
    public GameMenu targetMenu;
    private GameMenuManager menuManager;

    // Start is called before the first frame update
    void Start()
    {
        menuManager = GameMenuManager.GetInstance();
        GetComponent<Button>().onClick.AddListener(OnButtonPressed);
    }

    void OnButtonPressed()
    {
        menuManager.SwitchController(targetMenu);
    }
}
