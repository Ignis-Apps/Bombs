using Assets.Scripts.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenManager : AbstractScreenManager
{
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button LeaderboardButton;
    [SerializeField] private Button ShopButton;
    [SerializeField] private Button SettingsButton;

    [SerializeField] private TextMeshProUGUI HighScoreText;
    [SerializeField] private GameObject HighScore;

    // Init (like Awake) when the script is initialized
    protected override void Init() {}

    private void OnEnable()
    {
        UpdateHighScore();
    }

    void Start()
    {
        PlayButton.onClick.AddListener(Play);
        LeaderboardButton.onClick.AddListener(ShowLeaderboard);
        ShopButton.onClick.AddListener(ShowShop);
        SettingsButton.onClick.AddListener(ShowSettings);

        UpdateHighScore();

        GpgsController.SignInPromptOnce();

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
            //GpgsController.SignInPromptOnce();
        }
    }

    private void Play()
    {
        if (gameData.TutorialWasPlayed)
        {
            screenManager.SwitchScreen(ScreenType.INGAME_OVERLAY);
        }
        else
        {
            screenManager.SwitchScreen(ScreenType.TUTORIAL_SCREEN);
        }
    }

    private void ShowLeaderboard()
    {
        GpgsController.ShowLeaderboadUI();
    }

    private void ShowShop()
    {
        screenManager.SwitchScreen(ScreenType.SHOP_SCREEN);
    }

    private void ShowSettings()
    {
        screenManager.SwitchScreen(ScreenType.SETTINGS_SCREEN);
    }

    private void UpdateHighScore()
    {
        if (gameData.HighScore > 0)
        {
            HighScoreText.text = "BEST - " + GameData.GetTimeString(gameData.HighScore);
            HighScore.SetActive(true);
        }
        else
        {
            HighScore.SetActive(false);
        }
            
    }
}
