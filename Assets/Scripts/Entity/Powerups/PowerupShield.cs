using Assets.Scriptable.powerups;
using UnityEngine;

namespace Assets.Scripts.Entity.Powerups
{
    class PowerupShield : Powerup
    {
        [SerializeField] private GameObject shieldPrefab;
        private GameObject shieldInstance;

        private ShieldConfiguration configuration;

        [SerializeField ]private ShieldConfiguration DUMMY_CONFIG;

        public override PowerUpConfiguration LoadConfiguration()
        {
            // TODO
            //-------------------------------------
            configuration = DUMMY_CONFIG;
            //-------------------------------------
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
