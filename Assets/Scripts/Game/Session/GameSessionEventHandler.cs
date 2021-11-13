
namespace Assets.Scripts.Game.Session
{
    public class GameSessionEventHandler
    {

        public delegate void OnCoinCollected();
        public delegate void OnCrystalCollected();
        public delegate void OnTargetMarkerReached();
        public delegate void OnPowerUpCollected(Powerup powerup);
        public static OnCoinCollected coinColltedDelegate;
        public static OnCrystalCollected crystalColltedDelegate;
        public static OnTargetMarkerReached targetMarkerReachedDelegate;
        public static OnPowerUpCollected powerUpCollected;

        public delegate void OnWaveComplete();
        public delegate void OnBombAvoided();
        public static OnWaveComplete waveCompleteDelegate;
        public static OnBombAvoided bombAvoidedDelegate;

        public delegate void OnCrateLanded();
        public delegate void OnCrateOpened();
        public delegate void OnPowerupStarted();
        public delegate void OnPowerupEnd();
        public static OnCrateLanded crateLandedDelegate;
        public static OnCrateOpened crateOpenedDelegate;
        public static OnPowerupStarted powerupStartedDelegate;
        public static OnPowerupEnd powerupEndDelegate;

        public delegate void OnSessionPaused();
        public delegate void OnSessionResumed();
        public delegate void OnSessionReset();        
        public static OnSessionPaused sessionPausedDelegate;
        public static OnSessionResumed sessionResumedDelegate;
        public static OnSessionReset sessionResetDelegate;

        public delegate void OnPlayerHit();
        public delegate void OnPlayerDied();
        public delegate void OnPlayerRevived();
        public delegate void OnGameEnd();
        public static OnPlayerHit playerHitDelegate;
        public static OnPlayerDied playerDiedDelegate;
        public static OnPlayerRevived playerRevivedDelegate;
        public static OnGameEnd gameEndDelegate;

        // Gehoert hier eigentlich nicht rein
        public delegate void OnInputStart();
        public delegate void OnInputStoped();
        public static OnInputStart inputStart;
        public static OnInputStoped inputStopped;

    }
}
