using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public enum GameMenu
{
    TITLE_SCREEN,
    INGAME_OVERLAY,
    PAUSE_SCREEN,
    GAME_OVER_SCREEN
};
public class GameMenuManager : Singleton<GameMenuManager>
{
    public GameMenu initialGameMenu;

    private List<GameMenuController> gameMenuControllerList;
    private GameMenuController currentActiveController;

    protected override void Awake()
    {
        gameMenuControllerList = GetComponentsInChildren<GameMenuController>().ToList();
        gameMenuControllerList.ForEach(controler => controler.gameObject.SetActive(false));
        SwitchController(initialGameMenu);
    }

    public void SwitchController(GameMenu nextGameMenu)
    {
        if(currentActiveController != null)
        {
            currentActiveController.gameObject.SetActive(false);
        }

        GameMenuController nextController = gameMenuControllerList.Find(controller => controller.menuType == nextGameMenu);
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
