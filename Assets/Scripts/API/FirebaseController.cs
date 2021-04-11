using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Crashlytics;
using Firebase.Analytics;
using System;
using Assets.Scripts.Game;

public class FirebaseController : Singleton<FirebaseController>
{
    private GameData gameData;

    private bool firebaseAvailable = false;
    private Firebase.FirebaseApp firebaseApp;

    public void Init()
    {
        gameData = GameData.GetInstance();

        UpdateConsent(gameData.ConsentCrashlytics, gameData.ConsentAnalytics);

        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                // Crashlytics will use the DefaultInstance, as well;
                // this ensures that Crashlytics is initialized.
                firebaseApp = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here for indicating that your project is ready to use Firebase.
                firebaseAvailable = true;
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }


    public void LogEvent(String name) {
        if(gameData.ConsentAnalytics)
        {
            UnityEngine.Analytics.Analytics.CustomEvent(name);
            
            if (firebaseAvailable)
            {
                FirebaseAnalytics.LogEvent(name);
            }
        }
        
    }

    public void UpdateConsent(bool crashlytics, bool analytics)
    {
        Crashlytics.IsCrashlyticsCollectionEnabled = crashlytics;
        FirebaseAnalytics.SetAnalyticsCollectionEnabled(analytics);
    }

    private void SetRemoteConfigDefaults()
    {
        // Skins
        /* defaults.Add("shop_skins_1_name", "Red");
        defaults.Add("shop_skins_1_price", 300);
        defaults.Add("shop_skins_1_currency", Currency.COINS);
        defaults.Add("shop_skins_1_name", "Blue");
        defaults.Add("shop_skins_1_price", 600);
        defaults.Add("shop_skins_1_currency", Currency.COINS);
        defaults.Add("shop_skins_1_name", "Green");
        defaults.Add("shop_skins_1_price", 450);
        defaults.Add("shop_skins_1_currency", Currency.COINS);
        defaults.Add("shop_skins_1_name", "Test");
        defaults.Add("shop_skins_1_price", 40);
        defaults.Add("shop_skins_1_currency", Currency.CRYSTALS);

        // Scenes
        defaults.Add("shop_scenes_1_name", "Dessert");
        defaults.Add("shop_scenes_1_price", 1000);
        defaults.Add("shop_scenes_1_currency", Currency.COINS);
        defaults.Add("shop_scenes_1_name", "Island");
        defaults.Add("shop_scenes_1_price", 2000);
        defaults.Add("shop_scenes_1_currency", Currency.COINS);
        defaults.Add("shop_scenes_1_name", "To the Moon");
        defaults.Add("shop_scenes_1_price", 4000);
        defaults.Add("shop_scenes_1_currency", Currency.COINS);


        // Upgrades
        defaults.Add("shop_upgrades_1_name", "Turrent");
        defaults.Add("shop_upgrades_1_lvl_1_price", 200);
        defaults.Add("shop_upgrades_1_lvl_1_currency", Currency.COINS);
        defaults.Add("shop_upgrades_1_lvl_2_price", 400);
        defaults.Add("shop_upgrades_1_lvl_2_currency", Currency.COINS);
        defaults.Add("shop_upgrades_1_lvl_3_price", 100);
        defaults.Add("shop_upgrades_1_lvl_3_currency", Currency.CRYSTALS);

        defaults.Add("shop_upgrades_1_name", "Slow Motion");
        defaults.Add("shop_upgrades_1_lvl_1_price", 1000);
        defaults.Add("shop_upgrades_1_lvl_1_currency", Currency.COINS);
        defaults.Add("shop_upgrades_1_lvl_2_price", 2000);
        defaults.Add("shop_upgrades_1_lvl_2_currency", Currency.COINS);*/
    }

    private void LoadRemoteConfig()
    {
        /* Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults).ContinueWith(task => {
            // Defaults loaded!
        }); */


    }
}
