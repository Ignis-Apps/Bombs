using UnityEngine;

public class PowerUpConfiguration : ScriptableObject
{
    [SerializeField] private float powerupDuration;    
    public float GetDuration { get => powerupDuration; }

}
