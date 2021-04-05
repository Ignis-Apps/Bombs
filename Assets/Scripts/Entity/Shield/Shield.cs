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
        [SerializeField] private SpriteRenderer shieldSprite;
        [SerializeField] Sprite[] ShieldStates;
        
        private GameObject shieldTarget;

        private void Start()
        {
            shieldTarget = GameManager.GetInstance().Player;
            transform.position = shieldTarget.transform.position;
            shieldSprite.sprite = ShieldStates[Mathf.Min(hitpoints-1, ShieldStates.Length-1)];
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Bomb"))
            {
                OnShieldHit();
            }
        }

        private void OnShieldHit()
        {
            
            if (--hitpoints == 0)
            {
                Powerup.CurrentActivePowerup.DeactivatePowerup();
                return;
            }
            shieldSprite.sprite = ShieldStates[Mathf.Min(hitpoints - 1, ShieldStates.Length - 1)];
        }

        private void FixedUpdate()
        {
            transform.position = shieldTarget.transform.position;
        }

    }
}
