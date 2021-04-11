using Assets.Scripts.Game;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScreenManager : AbstractScreenManager
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Button privacySettingsButton;
    [SerializeField] Button saveButton;


    // Init (like Awake) when the script is initialized
    protected override void Init() {}

    // Start is called before the first frame update
    protected void Start() 
    {
        musicSlider.normalizedValue = gameData.VolumeMusic;
        sfxSlider.normalizedValue = gameData.VolumeSFX;

        privacySettingsButton.onClick.AddListener(ShowPrivacySettings);
        saveButton.onClick.AddListener(Save);
    }

    private void ShowPrivacySettings()
    {
        screenManager.SwitchScreen(ScreenType.PRIVACY_SCREEN);
        PrivacyScreenManager privacyScreenManager = (PrivacyScreenManager) screenManager.getScreenManager(ScreenType.PRIVACY_SCREEN);
        privacyScreenManager.ShowSettingsDialog(ScreenType.SETTINGS_SCREEN);
    }

    private void Save()
    {
        gameData.VolumeMusic = musicSlider.normalizedValue;
        gameData.VolumeSFX = sfxSlider.normalizedValue;
        gameData.SaveData();
        screenManager.SwitchScreen(ScreenType.TITLE_SCREEN);
    }
}
