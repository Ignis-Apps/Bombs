using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Import Firebase
using Firebase;
using Firebase.Crashlytics;
using Firebase.Analytics;

public class FirebaseController : MonoBehaviour
{
    private bool firebaseEnabled = false;
    private Firebase.FirebaseApp app;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize Firebase

        Firebase.FirebaseApp.LogLevel = Firebase.LogLevel.Debug;

        Debug.LogWarning("Begin");

        if (PlayerPrefs.GetInt("consent_crashreporting", 0) == 0)
        {
            //Crashlytics.IsCrashlyticsCollectionEnabled = false;
        } else
        {
            Crashlytics.IsCrashlyticsCollectionEnabled = true;
        }

        Debug.LogWarning("Middle");

        if (PlayerPrefs.GetInt("consent_analytics", 0) == 0)
        {
            //FirebaseAnalytics.SetAnalyticsCollectionEnabled(false);
        }
        else
        {
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        }

        Debug.LogWarning("END");

        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                // Crashlytics will use the DefaultInstance, as well;
                // this ensures that Crashlytics is initialized.
                app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here for indicating that your project is ready to use Firebase.
                firebaseEnabled = true;
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
