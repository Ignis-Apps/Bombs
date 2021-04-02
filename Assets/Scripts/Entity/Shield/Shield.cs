using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entity.Shield
{
    class Shield : MonoBehaviour
    {
        [SerializeField] private int hitpoints;
        
        private GameObject shieldTarget;

        private void Start()
        {
            shieldTarget = GameManager.GetInstance().Player;
            transform.position = shieldTarget.transform.position;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Bomb"))
            {
                if(--hitpoints == 0)
                {
                    Powerup.CurrentActivePowerup.DeactivatePowerup();
                    
                }                    
            }
        }

        private void FixedUpdate()
        {
            transform.position = shieldTarget.transform.position;
        }

    }
}
