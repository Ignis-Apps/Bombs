using Assets.Scriptable;
using Assets.Scripts.Game.Session;

namespace Assets.Scripts.Game
{
    /*
     *  Keeps track of the progress of the current game round
     *  
     *  TO BE CONTINUED
     */
    public class ProgressStats
    {
        private GameWave currentGameWave;
        public float currentDayProgress;
        public float currentWaveProgress;

        public float SecoundsSurvived { get; set; }
        public int BombsAvoided { get; private set; }
        public int SurvivedWaves { get; private set; }

        public ProgressStats()
        {
            Subscribe();
        }

        ~ProgressStats()
        {
            UnSubscribe();
        }

        public void Subscribe() {
            GameSessionEventHandler.waveCompleteDelegate += OnWaveComplete;
            GameSessionEventHandler.sessionResetDelegate += OnReset;
            GameSessionEventHandler.bombAvoidedDelegate += OnBombDodged;
        
        }
        public void UnSubscribe() {
            GameSessionEventHandler.waveCompleteDelegate -= OnWaveComplete;
            GameSessionEventHandler.sessionResetDelegate -= OnReset;
            GameSessionEventHandler.bombAvoidedDelegate -= OnBombDodged;
        }


        private void OnWaveComplete()
        {
            SurvivedWaves += 1;
        }

        private void OnBombDodged()
        {
            BombsAvoided++;
        }

        private void OnReset()
        {
            currentDayProgress = 0f;
            currentWaveProgress = 0f;
            
            SecoundsSurvived = 0f;
            SurvivedWaves = 0;
            BombsAvoided = 0;
        }

    }

    
}
