using UnityEngine;
using UnityEngine.UI;

public class SkinSwitchScreenManager : AbstractScreenManager
{
    [SerializeField] Button nextHeadButton;
    [SerializeField] Button prevHeadButton;

    [SerializeField] Button nextBodyButton;
    [SerializeField] Button prevBodyButton;

    [SerializeField] Button nextLegButton;
    [SerializeField] Button prevLegButton;

    [SerializeField] Button nextShoeButton;
    [SerializeField] Button prevShoeButton;

    [SerializeField] Button continueButton;

    private PlayerSkinChanger skinChanger;


    // Init (like Awake) when the script is initialized
    protected override void Init() {
        //skinChanger = gameManager.getPlayer().PlayerSkinChanger;
    }

    void Start()
    {

        skinChanger = gameManager.getPlayer().PlayerSkinChanger;

        nextHeadButton.onClick.AddListener(skinChanger.nextHead);
        prevHeadButton.onClick.AddListener(skinChanger.previousHead);

        nextBodyButton.onClick.AddListener(skinChanger.nextUpperBody);
        prevBodyButton.onClick.AddListener(skinChanger.previousUpperBody);

        nextLegButton.onClick.AddListener(skinChanger.nextLegs);
        prevLegButton.onClick.AddListener(skinChanger.previousLegs);

        nextShoeButton.onClick.AddListener(skinChanger.nextShoes);
        prevShoeButton.onClick.AddListener(skinChanger.previousShoes);

        continueButton.onClick.AddListener(ShowStartScreen);

    }

    private void ShowStartScreen()
    {
        screenManager.SwitchScreen(ScreenType.TITLE_SCREEN);
    }

}
