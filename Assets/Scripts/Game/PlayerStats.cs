
namespace Assets.Scripts.Game
{
    public class PlayerStats
    {

        // Collected items
        private int remainingLives;
        private int collectedCoins;
        private int collectedScorePoints;
        private int collectedCrystals;

        // Player state
        private bool isPlayerProtected;
        private bool isPlayerMoving;
        private bool isPlayerNearCrate;

        // Player attributes
        private float playerSpeedBase;          // Default player speed [Including Shop Upgrades]
        private float playerSpeedPowerup;       // Speed multiplier of the current powerup
        private float playerSpeedWave;          // Speed multiplier of the current wave

        private int revied;                     // Amount of revives this player has received

        public PlayerStats()
        {
            Init();
        }

        public void Reset()
        {
            Init();
        }

        private void Init()
        {
            remainingLives = 3;
            collectedCoins = 0;
            collectedScorePoints = 0;
            collectedCrystals = 0;
            revied = 0;

            isPlayerProtected = false;
            isPlayerMoving = false;
            isPlayerNearCrate = false;

            playerSpeedBase = 2.5f;
            playerSpeedPowerup = 1f;
            playerSpeedWave = 1f;

        }

        public int Lifes { get => remainingLives; set { remainingLives = value; } }
        public int Coins { get => collectedCoins; set { collectedCoins = value; } }
        public int Score { get => collectedScorePoints; set { collectedScorePoints = value; } }
        public int Crystals { get => collectedCrystals; set { collectedCrystals = value; } }
        
        public int AmountOfRevives { get => revied; set { revied = value; } }

        public float Speed { get => playerSpeedBase * playerSpeedWave * playerSpeedPowerup; }
        public float SpeedBase { get => playerSpeedBase; set { playerSpeedBase = value; } }
        public float SpeedPowerup { get => playerSpeedPowerup; set { playerSpeedPowerup = value; } }
        public float SpeedWave { get => playerSpeedWave; set { playerSpeedWave = value; } }

        public bool IsProtected { get => isPlayerProtected; set { isPlayerProtected = value; } }
        public bool IsMoving { get => isPlayerMoving; set { isPlayerMoving = value; } }
        public bool IsNearCrate { get => isPlayerNearCrate; set { isPlayerNearCrate = value; } }

    }
}
