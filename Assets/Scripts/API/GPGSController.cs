using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;



public class GPGSController : Singleton<GPGSController>
{
    private bool userIsSignedIn = false;

    private const string testAchievementId = "CgkI2cKjs6AOEAIQAQ";
    private const string testLeaderboardId = "CgkI2cKjs6AOEAIQAg";

    public void Init() {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();

        PlayGamesPlatform.InitializeInstance(config);

        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;

        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();
    }

    public void SignInPromptOnce()
    {
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) => {
            if (result == SignInStatus.Success)
            {
                userIsSignedIn = true;
            }
            else
            {
                userIsSignedIn = false;
                Debug.LogWarning("Google Play Games Sign-In Failed! Error: " + result);
            }
        });
    }

    public void SignInPromptAlways()
    {
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, (result) => {
            if (result == SignInStatus.Success)
            {
                userIsSignedIn = true;
            }
            else
            {
                userIsSignedIn = false;
                Debug.LogWarning("Google Play Games Sign-In Failed! Error: " + result);
            }
        });
    }

    public void UnlockTextAchivment()
    {
        if (userIsSignedIn)
        {
            Social.ReportProgress(testAchievementId, 100.0f, (bool success) => {
                if (success)
                {
                    Debug.Log("Test Achievement successfully unlocked!");
                }
                else
                {
                    Debug.LogWarning("Unlocking Test Achievement failed!");
                }
            });
        }
    }

    public void UpdateScoreTestLeaderboard(int timeInMs)
    {
        if (userIsSignedIn)
        {
            Social.ReportScore(timeInMs, testLeaderboardId, (bool success) => {
                if (success)
                {
                    Debug.Log("Successfully submitted score to the Test Leaderboard! Score: " + timeInMs);
                }
                else
                {
                    Debug.LogWarning("Score submission to the Test Leaderboard failed! Score: " + timeInMs);
                }
            });
        }
    }

    public void ShowAchivmentUI()
    {
        if (!userIsSignedIn)
        {
            SignInPromptAlways();
        } else
        {
            Social.ShowAchievementsUI();
        }
    }

    public void ShowLeaderboardUI()
    {
        if (!userIsSignedIn)
        {
            SignInPromptAlways();
        }
        else
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI(testLeaderboardId);
        }
    }

}
