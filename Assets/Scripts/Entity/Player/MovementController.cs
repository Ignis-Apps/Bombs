using Assets.Scripts.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour 
{
    [SerializeField] private float baseMovementSpeed;
    [SerializeField] private float breakDamping;
    [SerializeField] private float accelerationDamping;

    private ControllerState controllerState;
    private GameManager gameManager;
    private Rigidbody2D body;
    private Animator animator;
    
    // True since all player sprites face right
    private bool facingRight = true;
    
    // Flag that gets enabled when a new target position is set.
    private bool shouldMoveTorwardsTarget;
    
    // Max movement speed ( baseMovementSpeed * PlayerSpeedFactor )
    private float totalMovementSpeed;

    // Buffer for movement smoothing
    private Vector2 currentVelocity = Vector2.zero; 
    
    private Vector2 targetVelocity = Vector2.zero;

    // Object will move torwards this position (only horizontally). Requires enabled FLAG
    private Vector2 targetPosition;

    // Holds the velocity of the object. It doesnt matter if it is moving with physics or by translations.
    // Required for variable animation speeds.
    private float virtualVelocity;   


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameManager = GameManager.GetInstance();
        controllerState = ControllerState.GetInstance();       
    
    }
    public void Update()
    {
        if(controllerState.currentMode == ControllerMode.PLAYER)
        {
            Move(new Vector2(controllerState.stickPositionX, controllerState.stickPositionY));
        }
        else
        {
            Move(Vector2.zero);
        }
        
        totalMovementSpeed = baseMovementSpeed * gameManager.PlayerSpeedFactor;
        
        animator.SetFloat("playerSpeed", virtualVelocity);
        animator.SetBool("playerInFrontOfCrate", gameManager.IsPlayerNearCrate);
        gameManager.IsPlayerMoving = (virtualVelocity > 0);
    }

    public void FixedUpdate()
    {
        if (shouldMoveTorwardsTarget)
        {
            MoveTorwardsTargetPosition();
            return;
        }

        // Apply diffrent dampings based on the acceloration direction.
        float damping = (targetVelocity.magnitude > currentVelocity.magnitude) ? accelerationDamping : breakDamping;
        body.velocity = Vector2.SmoothDamp(body.velocity, targetVelocity, ref currentVelocity, damping);

        // Movement is physics based. Therefore copy the velocity from the rigid body
        virtualVelocity = Mathf.Abs(body.velocity.x);
    }

    public void Move(Vector2 direction)
    {
        targetVelocity = direction * totalMovementSpeed;
        FlipIfRequired(targetVelocity.x);
        
    }

    public void SetTargetPosition(Vector2 targetPosition)
    {
        this.targetPosition = targetPosition;
        shouldMoveTorwardsTarget = true;
    }

    public void ClearTargetPosition()
    {
        //Debug.Log("CLEARING");
        shouldMoveTorwardsTarget = false;
    }

    private void MoveTorwardsTargetPosition()
    {

        targetPosition.y = transform.localPosition.y;

        if(Mathf.Abs(targetPosition.x - body.position.x) < 0.001f){ ClearTargetPosition(); }

        Vector2 nextPosition = Vector2.MoveTowards(transform.localPosition, targetPosition, totalMovementSpeed * Time.deltaTime);
        
        float translationVelocity = (nextPosition.x - body.position.x) / Time.deltaTime;        
        virtualVelocity = Mathf.Abs(translationVelocity);

        FlipIfRequired(translationVelocity);

        body.position = new Vector2(nextPosition.x, body.position.y);
      
    }

    public void Stop()
    {
        targetVelocity = Vector2.zero;    
    }

    private void FlipIfRequired(float direction)
    {
        if (direction > 0 && !facingRight || direction < 0 && facingRight)
        {
            Flip();
        }
    }
    
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

}
