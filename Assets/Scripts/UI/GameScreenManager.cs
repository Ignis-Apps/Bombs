using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Game;
using Assets.Scripts.UI;

public class GameScreenManager : Singleton<GameScreenManager>
{
    public List<GameObject> screenPrefabs;
    public Assets.Scripts.UI.ScreenType initialScreen;

    private List<ScreenController> gameScreenControllerList;
    private ScreenController currentActiveController;
   

    protected override void Awake()
    {
        screenPrefabs.ForEach(menu => Instantiate(menu, this.gameObject.transform));

        gameScreenControllerList = GetComponentsInChildren<ScreenController>().ToList();       
        gameScreenControllerList.ForEach(controler => controler.gameObject.SetActive(false));        
        gameScreenControllerList.ForEach(controler => controler.gameObject.GetComponent<RectTransform>().position = transform.position);        
        SwitchScreen(initialScreen);        
    }

    public void SwitchScreen(ScreenType nextScreen)
    {
        Debug.Log("SwitchScreen: " + nextScreen);
        if(currentActiveController != null)
        {
            currentActiveController.gameObject.SetActive(false);
        }

        ScreenController nextController = gameScreenControllerList.Find(controller => controller.screenType == nextScreen);
        if(nextController == null)
        {
            return;
        }

        nextController.gameObject.SetActive(true);
        currentActiveController = nextController;
        
    }

    public bool CanPlayerMove()
    {
        return currentActiveController.allowPlayerMovement;
    }
}
