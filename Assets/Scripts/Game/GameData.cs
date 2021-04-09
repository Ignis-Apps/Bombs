using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum Currency
{
    COINS,
    CRYSTALS
}

namespace Assets.Scripts.Game
{
    class GameData : Singleton<GameData>
    {
        private int highScore;
        private int coinBalance;
        private int crystalBalance;

        //Selected Skin/Scene
        private Skin selectedSkin;
        private Scene selectedScene;

        //Settings
        private float volumeMusic;
        private float volumeSFX;
        private int consentAnalytics;
        private int consentCrashlytics;
        private int consentPersonalisedAds;

        // Conatins the Skins/Scences/Upgrades a user owns
        private Dictionary<int, bool> skinInventory = new Dictionary<int, bool>();
        private Dictionary<int, bool> sceneInventory = new Dictionary<int, bool>();
        private Dictionary<int, int> upgradeInventory = new Dictionary<int, int>();

        public int HighScore { get => highScore; set { if (value > highScore) { highScore = value; } } }
        public int CoinBalance { get => coinBalance; set { coinBalance = value; } }
        public int CrystalBalance { get => crystalBalance; set { crystalBalance = value; } }
        public Skin SelectedSkin { get => selectedSkin; set { selectedSkin = value; } }
        public Scene SelectedScene { get => selectedScene; set { selectedScene = value; } }
        public float VolumeMusic { get => volumeMusic; set { volumeMusic = value; } }
        public float VolumeSFX { get => volumeSFX; set { volumeSFX = value; } }
        public int ConsentAnalytics { get => consentAnalytics; set { consentAnalytics = value; } }
        public int ConsentCrashlytics { get => consentCrashlytics; set { consentCrashlytics = value; } }
        public int ConsentPersonalisedAds { get => consentPersonalisedAds; set { consentPersonalisedAds = value; } }

        public bool getSkinInventory(int id)
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

        public bool getSceneInventory(int id)
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

        public int getUpgradeInventory(int id)
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

        public void LoadData()
        {
            highScore = PlayerPrefs.GetInt("highScore", 0);
            coinBalance = PlayerPrefs.GetInt("coinBalance", 0);
            crystalBalance = PlayerPrefs.GetInt("crystalBalance", 0);

            volumeMusic = PlayerPrefs.GetFloat("volumeMusic", 0);
            volumeSFX = PlayerPrefs.GetFloat("volumeSFX", 0);
            consentAnalytics = PlayerPrefs.GetInt("consentAnalytics", 0);
            consentCrashlytics = PlayerPrefs.GetInt("consentCrashlytics", 0);
            consentPersonalisedAds = PlayerPrefs.GetInt("consentPersonalisedAds", 0);
        }

        public void SaveData()
        {
            PlayerPrefs.SetInt("highScore", highScore);
            PlayerPrefs.SetInt("coinBalance", coinBalance);
            PlayerPrefs.SetInt("crystalBalance", crystalBalance);

            PlayerPrefs.SetFloat("volumeMusic", volumeMusic);
            PlayerPrefs.SetFloat("volumeSFX", volumeSFX);
            PlayerPrefs.SetInt("consentAnalytics", consentAnalytics);
            PlayerPrefs.SetInt("consentCrashlytics", consentCrashlytics);
            PlayerPrefs.SetInt("consentPersonalisedAds", consentPersonalisedAds);
        }

    }
}
