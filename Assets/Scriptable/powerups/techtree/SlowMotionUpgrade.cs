using UnityEngine;
namespace Assets.Scriptable.powerups.techtree
{
    [CreateAssetMenu(menuName = "Powerup/Techtree/Slowmotion")]
    public class SlowMotionUpgrade : PowerupUpgrade
    {
        [SerializeField] SlowMotionConfiguration[] configurations;

        public override int GetAmountOfUpgrades()
        {
            return configurations.Length;
        }

        public SlowMotionConfiguration GetSlowMotionConfiguration()
        {

            int level = GetLevel();
            
            
            if(configurations.Length == 0)
            {
                Debug.LogError("No configuration has been set yet !");
            }
            
            if(level >= configurations.Length)
            {
                Debug.LogError("Upgrade level is bigger than the amount of available configurations !");
                level = configurations.Length - 1;
            }

            return configurations[level];

        }

    }
}
