using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuSwitcher : MonoBehaviour
{
    public Menu targetMenu;
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

    private void Update()
    {
        
    }
}
