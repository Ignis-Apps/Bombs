using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Game;
using Assets.Scripts.UI;

public class GameStateManager : Singleton<GameStateManager>
{
    public Menu initialGameMenu;

    private List<MenuController> gameMenuControllerList;
    private MenuController currentActiveController;
   

    protected override void Awake()
    {
       
        gameMenuControllerList = GetComponentsInChildren<MenuController>().ToList();       
        gameMenuControllerList.ForEach(controler => controler.gameObject.SetActive(false));        
        gameMenuControllerList.ForEach(controler => controler.gameObject.GetComponent<RectTransform>().position = transform.position);        
        SwitchController(initialGameMenu);
        
    }

    public void SwitchController(Menu nextGameMenu)
    {
        if(currentActiveController != null)
        {
            currentActiveController.gameObject.SetActive(false);
        }

        MenuController nextController = gameMenuControllerList.Find(controller => controller.menuType == nextGameMenu);
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
