
using UnityEngine;

namespace Assets.Scriptable.powerups
{
    [CreateAssetMenu(menuName = "Powerup/Slowmotion_Level")]
    public class SlowMotionConfiguration : PowerUpConfiguration
    {

        [SerializeField] private float slowMotionFactor;

        public float SlowMotionFactor { get => slowMotionFactor; }

    }
}
