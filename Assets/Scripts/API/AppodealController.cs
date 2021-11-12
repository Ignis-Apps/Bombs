using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using Assets.Scripts.Game;

public class AppodealController : Singleton<AppodealController>, IRewardedVideoAdListener
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

        Appodeal.disableLocationPermissionCheck();
        Appodeal.disableWriteExternalStoragePermissionCheck();
        Appodeal.muteVideosIfCallsMuted(true);
        Appodeal.setRewardedVideoCallbacks(this);

        //Deactivate not used ad networks
        Appodeal.disableNetwork(AppodealNetworks.A4G);
        Appodeal.disableNetwork(AppodealNetworks.SMAATO);
        Appodeal.disableNetwork(AppodealNetworks.FACEBOOK);
        Appodeal.disableNetwork(AppodealNetworks.YANDEX);
        Appodeal.disableNetwork(AppodealNetworks.AMAZON_ADS);
        Appodeal.disableNetwork(AppodealNetworks.STARTAPP);

        Appodeal.initialize(appKey, Appodeal.REWARDED_VIDEO, gameData.ConsentPersonalisedAds);
    }

    public void UpdateConsent(bool consent)
    {
        Appodeal.updateConsent(consent);
    }

    public bool IsRewardedVideoLoaded()
    {
        return Appodeal.isLoaded(Appodeal.REWARDED_VIDEO);
    }

    public bool ShowRewardedVideo()
    {
        if (IsRewardedVideoLoaded())
        {
            Appodeal.show(Appodeal.REWARDED_VIDEO);
            return true;
        }
        return false;
    }

    public void onRewardedVideoLoaded(bool isPrecache) {
        videoIsLoaded = true;
        print("Video loaded"); 
    } //Called when rewarded video was loaded (precache flag shows if the loaded ad is precache). 
    public void onRewardedVideoFailedToLoad() {
        videoIsLoaded = false;
        print("Video failed"); 
    } // Called when rewarded video failed to load 
    public void onRewardedVideoShowFailed() {
        videoCanceled = true;
        print("Video show failed"); 
    } // Called when rewarded video was loaded, but cannot be shown (internal network errors, placement settings, or incorrect creative) 
    public void onRewardedVideoShown() { print("Video shown"); } // Called when rewarded video is shown 
    public void onRewardedVideoClicked() { print("Video clicked"); } // Called when reward video is clicked 
    public void onRewardedVideoClosed(bool finished) {
        videoCanceled = !finished;
        print("Video closed"); 
    } // Called when rewarded video is closed 
    public void onRewardedVideoFinished(double amount, string name) {
        videoFinished = true;
        print("Reward: " + amount + " " + name);
    } // Called when rewarded video is viewed until the end 
    public void onRewardedVideoExpired() {
        videoIsLoaded = false;
        print("Video expired"); 
    } //Called when rewarded video is expired and can not be shown
}
