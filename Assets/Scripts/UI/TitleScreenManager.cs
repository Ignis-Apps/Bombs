using Assets.Scripts.Game;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenManager : AbstractScreenManager
{
    [SerializeField] Button playButton;
    [SerializeField] Button leaderboardButton;
    [SerializeField] Button shopButton;
    [SerializeField] Button settingsButton;

    // Init (like Awake) when the script is initialized
    protected override void Init() {}

    void Start()
    {
        playButton.onClick.AddListener(Play);
        leaderboardButton.onClick.AddListener(ShowLeaderboard);
        shopButton.onClick.AddListener(ShowShop);
        settingsButton.onClick.AddListener(ShowSettings);

        //Check for Consent
        if (!gameData.ConsentIsSet)
        {
            Debug.Log("Consent was not set jet!");
            screenManager.SwitchScreen(ScreenType.PRIVACY_SCREEN);
            PrivacyScreenManager privacyScreenManager = (PrivacyScreenManager) screenManager.getScreenManager(ScreenType.PRIVACY_SCREEN);
            privacyScreenManager.ShowMainDialog(ScreenType.TITLE_SCREEN);
        }
    }

    private void Play()
    {
        screenManager.SwitchScreen(ScreenType.INGAME_OVERLAY);
    }

    private void ShowLeaderboard()
    {
        Debug.Log("Starting Google Play Games Leaderboard ...");
    }

    private void ShowShop()
    {
        screenManager.SwitchScreen(ScreenType.SHOP_SCREEN);
    }

    private void ShowSettings()
    {
        screenManager.SwitchScreen(ScreenType.SETTINGS_SCREEN);
    }
}
