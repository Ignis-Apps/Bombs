using UnityEngine;

namespace Assets.Scripts.Game
{
    [System.Serializable]
    public class GameWave
    {
        [SerializeField] private string waveName;
        [SerializeField] private int waveDuration;

        [SerializeField] private AnimationCurve spawnIntervall;

        [SerializeField] private AnimationCurve defaultBombInitialSpeed;
        [SerializeField] private AnimationCurve defaultBombMaxSpeed;       
        [SerializeField] private AnimationCurve homingBombInitialSpeed;
        [SerializeField] private AnimationCurve homingBombMaxSpeed;      
        [SerializeField] private AnimationCurve smallBombInitialSpeed;
        [SerializeField] private AnimationCurve smallBombMaxSpeed;

        [SerializeField] private int spawnGroupSizeMin;
        [SerializeField] private int spawnGroupSizeMax;

        [SerializeField] private float playerSpeedMultiplier;

        [SerializeField] private float spawnWeightDefaultBomb;
        [SerializeField] private float spawnWeightHomingBomb;
        [SerializeField] private float spawnWeightSmallBomb;

        [SerializeField] private bool endWaveWithCrate;

        public float GetSpawnInterval(float waveProgress)
        {
            return spawnIntervall.Evaluate(waveProgress);
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
            totalWeight += spawnWeightSmallBomb;

            float r = Random.Range(0, totalWeight);

            if(r < spawnWeightDefaultBomb) { return SpawnType.DEFAULT_BOMB; }
            r -= spawnWeightDefaultBomb;
            if (r < spawnWeightHomingBomb) { return SpawnType.HOMING_BOMB; }
            r -= spawnWeightHomingBomb;
            if (r < spawnWeightSmallBomb) { return SpawnType.SMALL_BOMB; }

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

        public bool SpawnCrateAtEnd()
        {
            return endWaveWithCrate;
        }

        public enum SpawnType
        {
            DEFAULT_BOMB,
            HOMING_BOMB,
            SMALL_BOMB
        }
    }

}
