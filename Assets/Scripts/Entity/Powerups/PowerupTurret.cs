using Assets.Scriptable.powerups.techtree;
using UnityEngine;

namespace Assets.Scripts.Entity.Powerups
{
    class PowerupTurret : Powerup
    {       
        [SerializeField] private TurretUpgrade turretUpgradeTree;
        [SerializeField] private GameObject turretPrefab;
        
        private GameObject turretInstance;
        private TurretConfiguration turretConfiguration;

        private float SPAWN_HEIGHT = 5f;    
        
        public override PowerUpConfiguration LoadConfiguration()
        {            
            turretConfiguration = turretUpgradeTree.GetTurretConfiguration();
            return turretConfiguration;
        }

        public override void OnPowerupActivate()
        {
            
            turretInstance = Instantiate(turretPrefab);
            turretInstance.GetComponent<Turret>().SetConfiguration(turretConfiguration);
            
            Transform playerTransform = gameManager.getPlayer().transform;           
            Vector3 spawnPosition = new Vector3(playerTransform.position.x, SPAWN_HEIGHT, 0);
                       
            turretInstance.transform.position = spawnPosition;       
            controllerState.currentMode = Control.ControllerMode.TURRET;

            gameManager.session.playerStats.IsProtected = true;

            CameraFollow followScript = Camera.main.GetComponent<CameraFollow>();
            followScript.SetTarget(turretInstance.transform);
       
        }

        public override void OnPowerupDeactivate()
        {
            CameraFollow followScript = Camera.main.GetComponent<CameraFollow>();
            followScript.SetTarget(gameManager.getPlayer().transform);
            
            gameManager.session.playerStats.IsProtected = false;

            controllerState.currentMode = Control.ControllerMode.PLAYER;
            Destroy(turretInstance);
        }
    }
}
