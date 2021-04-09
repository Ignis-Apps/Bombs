using Assets.Scripts.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Powerup : MonoBehaviour
{
    [HideInInspector] public static Powerup CurrentActivePowerup;    
    
    [HideInInspector] protected GameManager gameManager;
    [HideInInspector] protected ControllerState controllerState;

    //[SerializeField] private float powerupDurationSecounds;
    [Tooltip("Remaining time after deactivation till the destroyment")]
    [SerializeField] private float cleanUpTime;

    private PowerUpConfiguration powerUpConfiguration;

    protected float remaingTime;
    private bool powerupActive;

    // Usefull to prevent the player from picking up the powerup too early
    [SerializeField] private float pickupProtectionTime;    

    void Awake()
    {
        gameManager = GameManager.GetInstance();
        controllerState = ControllerState.GetInstance();
        powerUpConfiguration = LoadConfiguration();
        StartCoroutine(PreventPickup());
    }

    private IEnumerator PreventPickup()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D c in colliders) { c.enabled = false; }
        yield return new WaitForSeconds(pickupProtectionTime);
        foreach (Collider2D c in colliders) { c.enabled = true; }
    }

    private void FixedUpdate()
    {
        
        if(powerupActive && (remaingTime -= Time.deltaTime) < 0)
        {
            DeactivatePowerup();
        }
    }

    public void ActivatePowerup()
    {
        if (CurrentActivePowerup != null) { return; }

        powerupActive = true;
        remaingTime = powerUpConfiguration.GetDuration;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        CurrentActivePowerup = this;
        OnPowerupActivate();
    }

    public void DeactivatePowerup()
    {
        powerupActive = false;
        CurrentActivePowerup = null;
        OnPowerupDeactivate();
        Destroy(gameObject, cleanUpTime);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (powerupActive) { return; }

        if (collision.CompareTag("Player"))
        {
            ActivatePowerup();
        }
    }

    
    public float GetNormalisedProgress()
    {
        return 1f - (remaingTime / powerUpConfiguration.GetDuration);
    }

    public abstract PowerUpConfiguration LoadConfiguration();
    public abstract void OnPowerupActivate();
    public abstract void OnPowerupDeactivate();
}
