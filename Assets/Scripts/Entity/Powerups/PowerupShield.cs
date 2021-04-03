﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entity.Powerups
{
    class PowerupShield : Powerup
    {
        [SerializeField] private GameObject shieldPrefab;
        private GameObject shieldInstance;

        public override void OnPowerupActivate()
        {
            shieldInstance = Instantiate(shieldPrefab);
            gameManager.PlayerHasShield = true;
        }

        public override void OnPowerupDeactivate()
        {
            gameManager.PlayerHasShield = false;
            shieldInstance.GetComponent<Animator>().SetTrigger("Shield_Tear_Down_Trigger");
            Destroy(shieldInstance, 1);
        }
    }
}