using Assets.Scripts.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Powerup : MonoBehaviour
{
    [HideInInspector] public static Powerup CurrentActivePowerup;    
    [HideInInspector] protected GameManager gameManager;
    [HideInInspector] protected ControllerState controllerState;

    [SerializeField] private float powerupDurationSecounds;
    [Tooltip("Remaining time after deactivation till the destroyment")]
    [SerializeField] private float cleanUpTime;

    protected float remaingTime;
    private bool powerupActive;

    void Awake()
    {
        gameManager = GameManager.GetInstance();
        controllerState = ControllerState.GetInstance();
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
        remaingTime = powerupDurationSecounds;
        GetComponent<BoxCollider2D>().enabled = false;
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
/*
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (powerupActive) { return; }

        if (collision.CompareTag("Player"))
        {
            ActivatePowerup();
        }
    }
*/
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (powerupActive) { return; }

        if (collision.CompareTag("Player"))
        {
            ActivatePowerup();
        }
    }

    public abstract void OnPowerupActivate();
    public abstract void OnPowerupDeactivate();
}
