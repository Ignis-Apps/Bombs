using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entity.Powerups
{
    class PowerupTurret : Powerup
    {
        public GameObject turretPrefab;

        [SerializeField] private Transform spawnHeight;
        private GameObject turret;

        public override void OnPowerupActivate()
        {
            //turret = Instantiate(turretPrefab, gameManager.getPlayer().transform.position, Quaternion.identity);
            turret = Instantiate(turretPrefab);
            
            Transform playerTransform = gameManager.getPlayer().transform;           
            float playerRect = gameManager.getPlayer().GetComponent<Collider2D>().bounds.size.x;
            float turretRec  = turret.GetComponent<Collider2D>().bounds.size.x;
            
            float offset = (playerRect - turretRec) /2f;
            
            Vector3 spawnPosition = new Vector3(playerTransform.position.x + offset, 5, 0);
                       
            turret.transform.position = spawnPosition;
            
            controllerState.currentMode = Control.ControllerMode.TURRET;
            
        }

        public override void OnPowerupDeactivate()
        {
            controllerState.currentMode = Control.ControllerMode.PLAYER;
            Destroy(turret);
        }
    }
}
