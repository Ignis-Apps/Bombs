using UnityEngine;

[CreateAssetMenu(menuName = "Powerup/Turret_Level")]
public class TurretConfiguration : PowerUpConfiguration
{
    // Rounds per secound
    [SerializeField] private float turretRPS;

    // Turn angel
    [SerializeField] private float turretTurnAngle;
    
    // Initial bullet speed;
    [SerializeField] private float bulletInitialSpeed;

    // Bullet spread;
    [SerializeField] private float bulletSpread;
    
    public float RoundsPerSecound { get => turretRPS; }
    public float BulletStartSpeed { get => bulletInitialSpeed; }
    public float BulletSpread { get => bulletSpread; }
    public float TurnAngle { get => turretTurnAngle; }

}
