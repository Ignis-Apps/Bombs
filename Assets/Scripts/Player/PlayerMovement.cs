using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private CharacterController2D controller2D;
    
    [SerializeField]
    private Animator animator;
    
    [SerializeField]
    private float playerMovementSpeed;

    private float playerMovementDirection = 0f;
    private bool playerIsMoving;
    private Vector2 playerTargetPosition;
    private GameMenuManager gameMenuManager;

    void Start()
    {
        gameMenuManager = GameMenuManager.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {

        // FOR PC KEY_BINDING
        if (!Application.isMobilePlatform)
        {
            float inputDir = Input.GetAxisRaw("Horizontal");
            if (inputDir < 0)
            {
                playerTargetPosition = new Vector2(-1000, 0);
                StartMoving();
            }
            else if (inputDir > 0)
            {
                playerTargetPosition = new Vector2(1000, 0);
                StartMoving();
            }
            else
            {
                StopMoving();
            }
            animator.SetFloat("playerSpeed", Mathf.Abs(playerMovementDirection));
            return;
        }
        // END OF PC BINDING


        if (!gameMenuManager.CanPlayerMove() || Input.touchCount <= 0) { return; }
        Touch touch = Input.GetTouch(0);
        playerTargetPosition = Camera.main.ScreenToWorldPoint(touch.position);

        switch (touch.phase)
        {
            case TouchPhase.Began:          
            case TouchPhase.Moved:
                StartMoving();
                break;
            case TouchPhase.Ended:
                StopMoving();
                break;
            default:
                break;
        }

        animator.SetFloat("playerSpeed", Mathf.Abs(playerMovementDirection));

             
    }

    private void StartMoving()
    {
        playerIsMoving = true;
    }

    private void StopMoving()
    {
        playerMovementDirection = 0;
        playerIsMoving = false;
        controller2D.Move(0, false, false);
    }

    void FixedUpdate() {

        if (!playerIsMoving) { return; }

        float distanceToTarget = playerTargetPosition.x - transform.position.x;
        float distanceToNextPosition = Mathf.Abs(playerMovementSpeed * Time.fixedDeltaTime);
            
        float EPSILON = 0.1f;

        if (distanceToTarget > EPSILON)
        {
            playerMovementDirection = 1f;
        }
        else if (distanceToTarget < -EPSILON)
        {
            playerMovementDirection = -1f;
        }
        else
        {
            playerMovementDirection = 0f;
        }

        if (Mathf.Abs(distanceToTarget) <= distanceToNextPosition)
        {
            playerMovementDirection *= Mathf.Abs(distanceToTarget / distanceToNextPosition);
            
        }

        controller2D.Move(playerMovementDirection* distanceToNextPosition, false, false);

    }
}
