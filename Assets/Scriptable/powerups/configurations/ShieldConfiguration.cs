
using UnityEngine;

namespace Assets.Scriptable.powerups
{
    [CreateAssetMenu(menuName = "Powerup/Shield_Level")]
    public class ShieldConfiguration : PowerUpConfiguration
    {

        [SerializeField] private int hitpoinst;

        public int Hitpoints { get => hitpoinst; }

    }
}
