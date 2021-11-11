
namespace Assets.Scripts.Game.Session
{
    public class GameSession
    {
        public PlayerStats playerStats = new PlayerStats();
        public ProgressStats progressStats = new ProgressStats();

        public bool IsTutotialRunning;

        public GameSession()
        {           
            GameSessionEventHandler.playerHitDelegate += OnPlayerHit;
           
        }

        ~GameSession()
        {    
            GameSessionEventHandler.playerHitDelegate -= OnPlayerHit;      
        }

        private void OnPlayerHit()
        {
            if (!playerStats.IsProtected) 
                playerStats.Lifes--;

            if (playerStats.Lifes == 0 && ScreenManager.GetInstance().CanPlayerMove())           
                GameSessionEventHandler.playerDiedDelegate();                
            
        }
           

    }
}
