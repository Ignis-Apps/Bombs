using UnityEngine;

namespace Assets.Scripts.Entity.Powerups
{
    class PowerupTurret : Powerup
    {       
        [SerializeField] private GameObject turretPrefab;
        
        private GameObject turretInstance;
        private TurretConfiguration turretConfiguration;

        private float SPAWN_HEIGHT = 5f;    
        
        [SerializeField] private TurretConfiguration DUMMY_CONFIG;

        public override PowerUpConfiguration LoadConfiguration()
        {
            // TODO
            //-------------------------------------
                turretConfiguration = DUMMY_CONFIG;
            //-------------------------------------

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

            CameraFollow followScript = Camera.main.GetComponent<CameraFollow>();
            followScript.SetTarget(turretInstance.transform);
       
        }

        public override void OnPowerupDeactivate()
        {
            CameraFollow followScript = Camera.main.GetComponent<CameraFollow>();
            followScript.SetTarget(gameManager.getPlayer().transform);

            controllerState.currentMode = Control.ControllerMode.PLAYER;
            Destroy(turretInstance);
        }
    }
}
