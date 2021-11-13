using Assets.Scripts.Control;
using Assets.Scripts.Game.Session;
using System.Collections;
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

    private bool deactivated;

    // Usefull to prevent the player from picking up the powerup too early [Crate Opening]
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
        deactivated = true;
        CurrentActivePowerup = null;
        OnPowerupDeactivate();
        Destroy(gameObject, cleanUpTime);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (powerupActive) { return; }

        if (collision.CompareTag("Player"))
        {           

            if (GameSessionEventHandler.powerUpCollected != null)
                GameSessionEventHandler.powerUpCollected(this);

            if (deactivated)
                return;

            ActivatePowerup();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {            
            foreach (Collider2D collider in GetComponents<Collider2D>())
            {
                collider.isTrigger = true;
            }
            
            Rigidbody2D body = GetComponent<Rigidbody2D>();
            transform.position = new Vector3(transform.position.x, GameManager.GetInstance().GroundTransform.position.y + GetComponent<SpriteRenderer>().bounds.size.y / 2f, transform.position.z);
            body.velocity = Vector2.zero;
            body.bodyType = RigidbodyType2D.Static;
        }
    }


    public virtual float GetNormalisedProgress()
    {
        return 1f - (remaingTime / powerUpConfiguration.GetDuration);
    }

    public abstract PowerUpConfiguration LoadConfiguration();
    public abstract void OnPowerupActivate();
    public abstract void OnPowerupDeactivate();
}
