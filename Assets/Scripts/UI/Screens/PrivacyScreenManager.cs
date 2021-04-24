using UnityEngine;
using UnityEngine.UI;

public class PrivacyScreenManager: AbstractScreenManager
{
    //Main Dialog
    [SerializeField] Button acceptAllMainButton;
    [SerializeField] Button settingsButton;

    //Settings Dialog
    [SerializeField] Toggle personalizedAdsToggle;
    [SerializeField] Toggle analyticsToggle;
    [SerializeField] Toggle crashReportingToggle;
    [SerializeField] Button acceptAllSettingsButton;
    [SerializeField] Button acceptSelectedButton;

    //Warning Dialog
    [SerializeField] Button continueAnywayButton;
    [SerializeField] Button backButton;

    private Transform mainDialog;
    private Transform settingsDialog;
    private Transform warningDialog;

    private ScreenType callbackScreen = ScreenType.TITLE_SCREEN;

    // Init (like Awake) when the script is initialized
    protected override void Init()
    {
        mainDialog = transform.Find("MainDialog");
        settingsDialog = transform.Find("SettingsDialog");
        warningDialog = transform.Find("WarningDialog");
    }

    public void Start()
    {
        personalizedAdsToggle.isOn = gameData.ConsentPersonalisedAds;
        analyticsToggle.isOn = gameData.ConsentAnalytics;
        crashReportingToggle.isOn = gameData.ConsentCrashlytics;

        acceptAllMainButton.onClick.AddListener(AcceptAll);
        settingsButton.onClick.AddListener(() => ShowSettingsDialog(callbackScreen));

        acceptAllSettingsButton.onClick.AddListener(AcceptAll);
        acceptSelectedButton.onClick.AddListener(ShowWarningDialog);

        continueAnywayButton.onClick.AddListener(AcceptSelected);
        backButton.onClick.AddListener(() => ShowSettingsDialog(callbackScreen));
    }



    public void ShowMainDialog(ScreenType callbackScreen)
    {
        Debug.Log("Show Main Consent Dialog...");
        this.callbackScreen = callbackScreen;
        mainDialog.gameObject.SetActive(true);
        settingsDialog.gameObject.SetActive(false);
        warningDialog.gameObject.SetActive(false);
    }

    public void ShowSettingsDialog(ScreenType callbackScreen)
    {
        Debug.Log("Show Settings Consent Dialog...");
        this.callbackScreen = callbackScreen;
        mainDialog.gameObject.SetActive(false);
        settingsDialog.gameObject.SetActive(true);
        warningDialog.gameObject.SetActive(false);
    }

    private void AcceptAll()
    {
        Debug.Log("Accepting All Purposes...");
        gameData.ConsentAnalytics = true;
        gameData.ConsentCrashlytics = true;
        gameData.ConsentPersonalisedAds = true;
        gameData.SaveConsent();

        mainDialog.gameObject.SetActive(false);
        settingsDialog.gameObject.SetActive(false);
        warningDialog.gameObject.SetActive(false);

        screenManager.SwitchScreen(callbackScreen);

        if(callbackScreen == ScreenType.TITLE_SCREEN)
        {
            //gpgsController.SignInPromptOnce();
            gpgsController.UnlockTextAchivment();
        }
    }

    private void AcceptSelected()
    {
        Debug.Log("Accepting Selected Purposes...");
        gameData.ConsentAnalytics = analyticsToggle.isOn;
        gameData.ConsentCrashlytics = crashReportingToggle.isOn;
        gameData.ConsentPersonalisedAds = personalizedAdsToggle.isOn;
        gameData.SaveConsent();

        mainDialog.gameObject.SetActive(false);
        settingsDialog.gameObject.SetActive(false);
        warningDialog.gameObject.SetActive(false);

        screenManager.SwitchScreen(callbackScreen);

        if (callbackScreen == ScreenType.TITLE_SCREEN)
        {
            //gpgsController.SignInPromptOnce();
        }
    }

    private void ShowWarningDialog()
    {
        if (!analyticsToggle.isOn || !crashReportingToggle.isOn || !personalizedAdsToggle.isOn)
        {
            Debug.Log("Show Warning Consent Dialog...");
            mainDialog.gameObject.SetActive(false);
            settingsDialog.gameObject.SetActive(false);
            warningDialog.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Skip Warning Consent Dialog...");
            AcceptSelected();
        }
    }

}
