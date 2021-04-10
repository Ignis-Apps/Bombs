using Assets.Scripts.Game;
using UnityEngine;
using UnityEngine.UI;

public class PrivacyScreenManger: MonoBehaviour
{
    private GameData gameData;
    private ScreenManager screenManager;

    private Transform mainDialog;
    private Transform settingsDialog;
    private Transform warningDialog;

    //Main Dialog
    [SerializeField] Button acceptAllMainButton;
    [SerializeField] Button settingsButton;

    //Settings Dialog
    [SerializeField] Toggle personalizedAdsToggle;
    [SerializeField] Toggle analyticsToggle;
    [SerializeField] Toggle crashReportingToggle;
    [SerializeField] Button AcceptAllSettingsButton;
    [SerializeField] Button AcceptSelectedButton;

    //Warning Dialog
    [SerializeField] Button continueAnywayButton;
    [SerializeField] Button backButton;


    // Start is called before the first frame update
    void Start()
    {
        gameData = GameData.GetInstance();
        screenManager = ScreenManager.GetInstance();

        mainDialog = transform.Find("MainDialog");
        settingsDialog = transform.Find("SettingsDialog");
        warningDialog = transform.Find("WarningDialog");

        acceptAllMainButton.onClick.AddListener(AcceptAll);
        settingsButton.onClick.AddListener(ShowSettingsDialog);

        AcceptAllSettingsButton.onClick.AddListener(AcceptAll);
        AcceptSelectedButton.onClick.AddListener(ShowWarningDialog);

        continueAnywayButton.onClick.AddListener(AcceptSelected);
        backButton.onClick.AddListener(ShowSettingsDialog);

        if (gameData.ConsentIsSet)
        {
            Debug.Log("Consent is set!");
            screenManager.SwitchScreen(ScreenType.TITLE_SCREEN);
        }
    }

    private void AcceptAll()
    {
        Debug.Log("Accepting All Purposes...");
        gameData.ConsentAnalytics = true;
        gameData.ConsentCrashlytics = true;
        gameData.ConsentPersonalisedAds = true;
        gameData.SaveConsent();
        screenManager.SwitchScreen(ScreenType.TITLE_SCREEN);
    }

    private void AcceptSelected()
    {
        Debug.Log("Accepting Selected Purposes...");
        gameData.ConsentAnalytics = analyticsToggle.isOn;
        gameData.ConsentCrashlytics = crashReportingToggle.isOn;
        gameData.ConsentPersonalisedAds = personalizedAdsToggle.isOn;
        gameData.SaveConsent();
        screenManager.SwitchScreen(ScreenType.TITLE_SCREEN);
    }

    private void ShowMainDialog()
    {
        Debug.Log("Show Main Consent Dialog...");
        mainDialog.gameObject.SetActive(true);
        settingsDialog.gameObject.SetActive(false);
        warningDialog.gameObject.SetActive(false);
    }

    private void ShowSettingsDialog()
    {
        Debug.Log("Show Settings Consent Dialog...");
        mainDialog.gameObject.SetActive(false);
        settingsDialog.gameObject.SetActive(true);
        warningDialog.gameObject.SetActive(false);
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
