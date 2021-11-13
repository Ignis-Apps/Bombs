using System.Collections.Generic;
using UnityEngine;

public enum Currency
{
    COINS,
    CRYSTALS
}

namespace Assets.Scripts.Game
{
    public class GameData : Singleton<GameData>
    {
        private int highScore;
        private int coinBalance;
        private int crystalBalance;

        //Selected Skin/Scene
        private int selectedSkin;
        private int selectedScene;

        //Settings
        private float volumeMusic;
        private float volumeSFX;

        private bool consentIsSet;
        private bool consentAnalytics;
        private bool consentCrashlytics;
        private bool consentPersonalisedAds;

        private bool tutorialWasPlayed;

        //
        private PlayerSkinChanger.PlayerSkinConfiguration playerSkin;

        // Conatins the Skins/Scences/Upgrades a user owns
        private Dictionary<int, bool> skinInventory = new Dictionary<int, bool>();
        private Dictionary<int, bool> sceneInventory = new Dictionary<int, bool>();
        private Dictionary<int, int> upgradeInventory = new Dictionary<int, int>();

        public int HighScore { get => highScore; set { if (value > highScore) { highScore = value; } } }
        public int CoinBalance { get => coinBalance; set { coinBalance = value; } }
        public int CrystalBalance { get => crystalBalance; set { crystalBalance = value; } }
        public int SelectedSkin { get => selectedSkin; set { selectedSkin = value; } }
        public int SelectedScene { get => selectedScene; set { selectedScene = value; } }
        public float VolumeMusic { get => volumeMusic; set { volumeMusic = value; } }
        public float VolumeSFX { get => volumeSFX; set { volumeSFX = value; } }
        public bool ConsentIsSet { get => consentIsSet; set { consentIsSet = value; } }
        public bool ConsentAnalytics { get => consentAnalytics; set { consentAnalytics = value; } }
        public bool ConsentCrashlytics { get => consentCrashlytics; set { consentCrashlytics = value; } }
        public bool ConsentPersonalisedAds { get => consentPersonalisedAds; set { consentPersonalisedAds = value; } }
        public bool TutorialWasPlayed { get => tutorialWasPlayed; set { tutorialWasPlayed = value; } }
        public PlayerSkinChanger.PlayerSkinConfiguration PlayerSkinInGame{ get => playerSkin; set { playerSkin = value; } }

        public bool HasSkinInInventory(int id)
        {
            if (skinInventory.TryGetValue(id, out bool result))
            {
                return result;
            }
            else
            {
                return false;
            }
        }

        public bool HasSceneInInventory(int id)
        {
            if (sceneInventory.TryGetValue(id, out bool result))
            {
                return result;
            }
            else
            {
                return false;
            }
        }

        public int HasUpgradeInInventory(int id)
        {
            if (upgradeInventory.TryGetValue(id, out int result))
            {
                return result;
            }
            else
            {
                return 0;
            }
        }

        public void SetSkinInventory(int id, bool value)
        {
            if (skinInventory.ContainsKey(id))
                skinInventory[id] = value;
            else
                skinInventory.Add(id, value);            
        }

        public void SetSceneInventory(int id, bool value)
        {
            sceneInventory.Add(id, value);
        }

        public void SetUpgradeInventory(int id, int value)
        {
            upgradeInventory.Add(id, value);
        }

        public static string GetTimeString(int seconds)
        {
            string output;
            int min = seconds / 60;
            int sec = seconds % 60;

            output = min + ":";
            if (sec < 10) { output += "0"; }
            output += sec;

            return output;
        }

        public static string GetCurrencyName(Currency c)
        {
            switch(c)
            {
                case Currency.COINS:
                    return "Coins";
                case Currency.CRYSTALS:
                    return "Crystals";
                default:
                    return "Unknown";
            }
        }

        public void LoadData()
        {
            highScore = PlayerPrefs.GetInt("highScore", 0);
            coinBalance = PlayerPrefs.GetInt("coinBalance", 0);
            crystalBalance = PlayerPrefs.GetInt("crystalBalance", 0);

            selectedSkin = PlayerPrefs.GetInt("selectedSkin", 0);
            selectedScene = PlayerPrefs.GetInt("selectedScene", 0);

            volumeMusic = PlayerPrefs.GetFloat("volumeMusic", 0);
            volumeSFX = PlayerPrefs.GetFloat("volumeSFX", 1);

            consentIsSet = PlayerPrefs.GetInt("consentIsSet", 0) != 0;
            consentAnalytics = PlayerPrefs.GetInt("consentAnalytics", 0) != 0;
            consentCrashlytics = PlayerPrefs.GetInt("consentCrashlytics", 0) != 0;
            consentPersonalisedAds = PlayerPrefs.GetInt("consentPersonalisedAds", 0) != 0;

            tutorialWasPlayed = PlayerPrefs.GetInt("tutorialFlag", 0) != 0;

            // Player Skin ( ingame )
            playerSkin = new PlayerSkinChanger.PlayerSkinConfiguration();
            string playerSkinString = PlayerPrefs.GetString("player_skin_configuration", "");
            if (playerSkinString.Length > 0) playerSkin.Load(playerSkinString);

            for (int i = 0; i < PlayerPrefs.GetInt("skin_size", 0); i++)
            {
                skinInventory.Add(i, System.Convert.ToBoolean(PlayerPrefs.GetInt("skin_" + i)));
            }

            for (int i = 0; i < PlayerPrefs.GetInt("scene_size", 0); i++)
            {
                sceneInventory.Add(i, System.Convert.ToBoolean(PlayerPrefs.GetInt("scene_" + i)));
            }

            for (int i = 0; i < PlayerPrefs.GetInt("upgrade_size", 0); i++)
            {
                upgradeInventory.Add(i, PlayerPrefs.GetInt("upgrade_" + i));
            }
        }

        public void SaveData()
        {
            Debug.Log("Saving Data ...");
            PlayerPrefs.SetInt("highScore", highScore);
            PlayerPrefs.SetInt("coinBalance", coinBalance);
            PlayerPrefs.SetInt("crystalBalance", crystalBalance);

            PlayerPrefs.SetInt("selectedSkin", selectedSkin);
            PlayerPrefs.SetInt("selectedScene", selectedScene);

            PlayerPrefs.SetFloat("volumeMusic", volumeMusic);
            PlayerPrefs.SetFloat("volumeSFX", volumeSFX);

            PlayerPrefs.SetInt("tutorialFlag", tutorialWasPlayed ? 1 : 0);

            PlayerPrefs.SetString("player_skin_configuration", playerSkin.Save());

            for (int i = 1; i < skinInventory.Count; i++)
            {
                if(skinInventory.TryGetValue(i, out bool result))
                    PlayerPrefs.SetInt("skin_" + i, result ? 1 : 0);
            }

            for (int i = 1; i < sceneInventory.Count; i++)
            {
                if(sceneInventory.TryGetValue(i, out bool result))
                    PlayerPrefs.SetInt("scene_" + i, result ? 1 : 0);
            }

            for (int i = 0; i < upgradeInventory.Count; i++)
            {
                if(upgradeInventory.TryGetValue(i, out int result))
                    PlayerPrefs.SetInt("upgrade_" + i, result);
            }

            PlayerPrefs.SetInt("skin_size", skinInventory.Count);
            PlayerPrefs.SetInt("scene_size", sceneInventory.Count);
            PlayerPrefs.SetInt("upgrade_size", upgradeInventory.Count);
        }

        public void SaveConsent()
        {
            Debug.Log("Saving Consent: " + consentPersonalisedAds + " / " + consentAnalytics + " / " + consentCrashlytics);
            PlayerPrefs.SetInt("consentIsSet", 1);
            PlayerPrefs.SetInt("consentAnalytics", consentAnalytics ? 1 : 0);
            PlayerPrefs.SetInt("consentCrashlytics", consentCrashlytics ? 1 : 0);
            PlayerPrefs.SetInt("consentPersonalisedAds", consentPersonalisedAds ? 1 : 0);

            AppodealController.GetInstance().UpdateConsent(consentPersonalisedAds);
            FirebaseController.GetInstance().UpdateConsent(consentCrashlytics, consentAnalytics);
        }

    }
}
