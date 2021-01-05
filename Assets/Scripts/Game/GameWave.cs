using UnityEngine;

namespace Assets.Scripts.Game
{
    [System.Serializable]
    public class GameWave
    {
        [SerializeField]
        private string waveName;
        [SerializeField]
        private int waveDuration;                       
        [SerializeField] 
        private float spawnIntervalLow;
        [SerializeField]
        private float spawnIntervalHigh;
        [SerializeField]
        private int spawnGroupSizeMin;
        [SerializeField]
        private int spawnGroupSizeMax;
        [SerializeField]
        private float playerSpeedMultiplier;

        [SerializeField]
        private float spawnWeightDefaultBomb;
        [SerializeField]
        private float spawnWeightHomingBomb;


        public float GetSpawnInterval(float waveProgress)
        {
            return (waveProgress * (spawnIntervalHigh - spawnIntervalLow)) + spawnIntervalLow;
        }

        public int GetSpawnGroupSize()
        {
            return Random.Range(spawnGroupSizeMin, spawnGroupSizeMax);
        }

        public SpawnType GetRandomSpawnType()
        {
            float totalWeight = 0;
            totalWeight += spawnWeightDefaultBomb;
            totalWeight += spawnWeightHomingBomb;

            float r = Random.Range(0, totalWeight);

            if(r < spawnWeightDefaultBomb) { return SpawnType.DEFAULT_BOMB; }
            r += spawnWeightDefaultBomb;
            if (r < spawnWeightHomingBomb) { return SpawnType.HOMING_BOMB; }
                       
            return SpawnType.HOMING_BOMB;
        }

        public float GetWaveDuration()
        {
            return waveDuration;
        }

        public float GetPlayerSpeedMultiplier()
        {
            return playerSpeedMultiplier;
        }

        public enum SpawnType
        {
            DEFAULT_BOMB,
            HOMING_BOMB
        }
    }

}
