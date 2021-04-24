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

        gpgsController.SignInPromptOnce();

        //Check for Consent
        if (!gameData.ConsentIsSet)
        {
            Debug.Log("Consent was not set jet!");
            screenManager.SwitchScreen(ScreenType.PRIVACY_SCREEN);
            PrivacyScreenManager privacyScreenManager = (PrivacyScreenManager) screenManager.getScreenManager(ScreenType.PRIVACY_SCREEN);
            privacyScreenManager.ShowMainDialog(ScreenType.TITLE_SCREEN);
        }
        else
        {
            //gpgsController.SignInPromptOnce();
        }
    }

    private void Play()
    {
        screenManager.SwitchScreen(ScreenType.INGAME_OVERLAY);
    }

    private void ShowLeaderboard()
    {
        gpgsController.ShowLeaderboadUI();
    }

    private void ShowShop()
    {
        //screenManager.SwitchScreen(ScreenType.SHOP_SCREEN);

        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, "Coming Soon...", 0);
                toastObject.Call("show");
            }));
        }
    }

    private void ShowSettings()
    {
        screenManager.SwitchScreen(ScreenType.SETTINGS_SCREEN);
    }
}
