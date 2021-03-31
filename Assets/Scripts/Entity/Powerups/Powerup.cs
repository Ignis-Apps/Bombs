using Assets.Scripts.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Powerup : MonoBehaviour
{
    [SerializeField] private float powerupDurationSecounds;

    private float remaingTime;
    private bool powerupActive;

    [HideInInspector] public static Powerup CurrentActivePowerup;
    [HideInInspector] public GameManager gameManager;
    [HideInInspector] public ControllerState controllerState;

    void Awake()
    {
        gameManager = GameManager.GetInstance();
        controllerState = ControllerState.GetInstance();
    }

    private void FixedUpdate()
    {
        if (!powerupActive) { return; }
        remaingTime -= Time.deltaTime;
        if(remaingTime < 0)
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
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
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
