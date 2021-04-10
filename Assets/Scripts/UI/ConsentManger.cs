using Assets.Scripts.Game;
using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsentManger : MonoBehaviour
{
    private GameData gameData;
    private GameScreenManager gameScreenManager;

    private Transform mainDialog;
    private Transform settingsDialog;
    private Transform warningDialog;

    //Main Dialog
    public Button AcceptAllMainButton;
    public Button SettingsButton;

    //Settings Dialog
    public Toggle personalizedAdsToggle;
    public Toggle analyticsToggle;
    public Toggle crashReportingToggle;
    public Button AcceptAllSettingsButton;
    public Button AcceptSelectedButton;

    //Warning Dialog
    public Button ContinueAnywayButton;
    public Button CancelButton;


    // Start is called before the first frame update
    void Start()
    {
        gameData = GameData.GetInstance();
        gameScreenManager = GameScreenManager.GetInstance();

        if (gameData.ConsentIsSet)
        {
            Debug.Log("Consent is set!");
            gameScreenManager.SwitchScreen(ScreenType.TITLE_SCREEN);
        } else
        {
            mainDialog = transform.Find("MainDialog");
            settingsDialog = transform.Find("SettingsDialog");
            warningDialog = transform.Find("WarningDialog");

            ShowMainDialog();

            AcceptAllMainButton.onClick.AddListener(AcceptAll);
            SettingsButton.onClick.AddListener(ShowSettingsDialog);

            AcceptAllSettingsButton.onClick.AddListener(AcceptAll);
            AcceptSelectedButton.onClick.AddListener(ShowWarningDialog);

            ContinueAnywayButton.onClick.AddListener(AcceptSelected);
            CancelButton.onClick.AddListener(ShowSettingsDialog);
        }
    }

    private void AcceptAll()
    {
        Debug.Log("Accepting All Purposes...");
        gameData.ConsentAnalytics = true;
        gameData.ConsentCrashlytics = true;
        gameData.ConsentPersonalisedAds = true;
        gameData.SaveConsent();
        gameScreenManager.SwitchScreen(ScreenType.TITLE_SCREEN);
    }

    private void AcceptSelected()
    {
        Debug.Log("Accepting Selected Purposes...");
        gameData.ConsentAnalytics = analyticsToggle.isOn;
        gameData.ConsentCrashlytics = crashReportingToggle.isOn;
        gameData.ConsentPersonalisedAds = personalizedAdsToggle.isOn;
        gameData.SaveConsent();
        gameScreenManager.SwitchScreen(ScreenType.TITLE_SCREEN);
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
