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

        if ( !gameMenuManager.CanPlayerMove() || Input.touchCount <= 0) { return; }

        Touch touch = Input.GetTouch(0);
        playerTargetPosition = Camera.main.ScreenToWorldPoint(touch.position);
        
        if(touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved) { playerIsMoving = true; }

            if (touch.phase == TouchPhase.Ended)
            {
            playerMovementDirection = 0;
            playerIsMoving = false;
            controller2D.Move(0, false, false);
            }
      

        animator.SetFloat("playerSpeed", Mathf.Abs(playerMovementDirection));        

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
