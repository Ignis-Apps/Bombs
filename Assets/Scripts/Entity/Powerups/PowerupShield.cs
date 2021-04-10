using Assets.Scriptable.powerups;
using Assets.Scriptable.powerups.techtree;
using UnityEngine;

namespace Assets.Scripts.Entity.Powerups
{
    public class PowerupShield : Powerup
    {
        [SerializeField] private ShieldUpgrade shieldUpgradeTree;
        
        [SerializeField] private GameObject shieldPrefab;
        private GameObject shieldInstance;

        private ShieldConfiguration configuration;

        public override PowerUpConfiguration LoadConfiguration()
        {   
            configuration = shieldUpgradeTree.GetShieldConfiguration();    
            return configuration;
        }

        public override void OnPowerupActivate()
        {
            shieldInstance = Instantiate(shieldPrefab);
            gameManager.playerStats.IsProtected = true;
        }

        public override void OnPowerupDeactivate()
        {
            gameManager.playerStats.IsProtected = false;
            shieldInstance.GetComponent<Animator>().SetTrigger("Shield_Tear_Down_Trigger");
            Destroy(shieldInstance, 1);
        }
    }
}
