using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScreenManager : Singleton<ScreenManager>
{
    [SerializeField] List<GameObject> screenPrefabs;
    [SerializeField] ScreenType initialScreen;

    private List<ScreenController> gameScreenControllerList;
    private ScreenController currentActiveController;
   

    protected override void Awake()
    {
        screenPrefabs.ForEach(screen => Instantiate(screen, this.gameObject.transform));

        gameScreenControllerList = GetComponentsInChildren<ScreenController>().ToList();       
        gameScreenControllerList.ForEach(controler => controler.gameObject.SetActive(false));        
        gameScreenControllerList.ForEach(controler => controler.gameObject.GetComponent<RectTransform>().position = transform.position);        
        SwitchScreen(initialScreen);        
    }

    public void SwitchScreen(ScreenType nextScreen)
    {
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

    public AbstractScreenManager getScreenManager(ScreenType type)
    {
        ScreenController controller = gameScreenControllerList.Find(controller => controller.screenType == type);
        AbstractScreenManager abstractScreenManager = controller.gameObject.GetComponent<AbstractScreenManager>();
        return abstractScreenManager;
    }


    public bool CanPlayerMove()
    {
        return currentActiveController.allowPlayerMovement;
    }
}
