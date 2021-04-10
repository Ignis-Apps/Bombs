using UnityEngine;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{
    private ScreenManager screenManager;

    [SerializeField] Button playButton;
    [SerializeField] Button leaderboardButton;
    [SerializeField] Button shopButton;
    [SerializeField] Button settingsButton;


    void Start()
    {
        screenManager = ScreenManager.GetInstance();

        playButton.onClick.AddListener(Play);
        leaderboardButton.onClick.AddListener(ShowLeaderboard);
        shopButton.onClick.AddListener(ShowShop);
        settingsButton.onClick.AddListener(ShowSettings);
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
