using Assets.Scriptable.powerups;
using UnityEngine;

namespace Assets.Scripts.Entity.Shield
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] private ShieldConfiguration defaultConfiguration;
        
        [SerializeField] private int hitpoints;
        [SerializeField] private SpriteRenderer shieldSprite;
        [SerializeField] Sprite[] ShieldStates;
        
        private GameObject shieldTarget;

        private ShieldConfiguration configuration;

        private void Start()
        {
            if (configuration == null) { configuration = defaultConfiguration; }

            hitpoints = configuration.Hitpoints;

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

        public void SetConfiguration(ShieldConfiguration configuration)
        {
            this.configuration = configuration;
        }

    }
}
