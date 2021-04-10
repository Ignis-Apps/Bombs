using UnityEngine;
namespace Assets.Scriptable.powerups.techtree
{
    [CreateAssetMenu(menuName = "Powerup/Techtree/Shield")]
    public class ShieldUpgrade : PowerupUpgrade
    {
        [SerializeField] ShieldConfiguration[] configurations;

        public override int GetAmountOfUpgrades()
        {
            return configurations.Length;
        }

        public ShieldConfiguration GetShieldConfiguration()
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
