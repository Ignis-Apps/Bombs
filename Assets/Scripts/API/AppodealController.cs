using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using Assets.Scripts.Game;
using System.Collections.Generic;

public class AppodealController : Singleton<AppodealController>, IRewardedVideoAdListener, IAppodealInitializationListener
{
    GameData gameData;

#if UNITY_EDITOR && !UNITY_ANDROID && !UNITY_IPHONE
    public static string appKey = "";
#elif UNITY_ANDROID
    public static string appKey = "4a4905ed78aa13c35c3ac027a32c7738c0c383fafd6b029d";
#elif UNITY_IPHONE
    // TODO: Add real key
    public static string appKey = "466de0d625e01e8811c588588a42a55970bc7c132649eede";
#else
    public static string appKey = "";
#endif
    public bool videoFinished;
    public bool videoCanceled;
    public bool videoIsLoaded;

    public void Init()
    {
        gameData = GameData.GetInstance();

        // TEST BUILD
        // Appodeal.setTesting(true);

       
        Appodeal.SetLocationTracking(false);
        Appodeal.MuteVideosIfCallsMuted(true);
        Appodeal.SetRewardedVideoCallbacks(this);

        //Deactivate not used ad networks
        Appodeal.DisableNetwork(AppodealNetworks.Facebook);
        Appodeal.DisableNetwork(AppodealNetworks.Yandex);

        Appodeal.Initialize(appKey, AppodealAdType.RewardedVideo, this);
    }

    public void UpdateConsent(bool consent)
    {
        if (consent)
        {
            Appodeal.UpdateCcpaConsent(CcpaUserConsent.OptIn);
            Appodeal.UpdateGdprConsent(GdprUserConsent.Personalized);
        }
        else
        {
            Appodeal.UpdateCcpaConsent(CcpaUserConsent.OptOut);
            Appodeal.UpdateGdprConsent(GdprUserConsent.NonPersonalized);
        }
    }

    public bool IsRewardedVideoLoaded()
    {
        return Appodeal.IsLoaded(AppodealAdType.RewardedVideo);
    }

    public bool ShowRewardedVideo()
    {
        if (IsRewardedVideoLoaded())
        {
            Appodeal.Show(AppodealAdType.RewardedVideo);
            return true;
        }
        return false;
    }

    public void OnInitializationFinished(List<string> errors) { }

    public void OnRewardedVideoLoaded(bool isPrecache) {
        videoIsLoaded = true;
        print("Video loaded"); 
    } //Called when rewarded video was loaded (precache flag shows if the loaded ad is precache). 
    public void OnRewardedVideoFailedToLoad() {
        videoIsLoaded = false;
        print("Video failed"); 
    } // Called when rewarded video failed to load 
    public void OnRewardedVideoShowFailed() {
        videoCanceled = true;
        print("Video show failed"); 
    } // Called when rewarded video was loaded, but cannot be shown (internal network errors, placement settings, or incorrect creative) 
    public void OnRewardedVideoShown() { print("Video shown"); } // Called when rewarded video is shown 
    public void OnRewardedVideoClicked() { print("Video clicked"); } // Called when reward video is clicked 
    public void OnRewardedVideoClosed(bool finished) {
        videoCanceled = !finished;
        print("Video closed"); 
    } // Called when rewarded video is closed 
    public void OnRewardedVideoFinished(double amount, string name) {
        videoFinished = true;
        print("Reward: " + amount + " " + name);
    } // Called when rewarded video is viewed until the end 
    public void OnRewardedVideoExpired() {
        videoIsLoaded = false;
        print("Video expired"); 
    } //Called when rewarded video is expired and can not be shown
}
