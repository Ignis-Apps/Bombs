using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private Animator animator; 
    [SerializeField] private float playerDefaultSpeed;

    private float playerMovementSpeed;
    private float playerMovementDirection = 0f;
    private bool playerIsMoving;
    private Vector2 playerTargetPosition;

    private GameMenuManager gameMenuManager;
    private GameManager gameManager;

    private Rigidbody2D playerBody;
    private Vector3 m_Velocity = Vector3.zero;

    private bool m_FacingRight = true;

    void Start()
    {
        gameMenuManager = GameMenuManager.GetInstance();
        gameManager = GameManager.GetInstance();
        playerBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        playerMovementSpeed = gameManager.PlayerSpeedFactor * playerDefaultSpeed;

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
            animator.SetFloat("playerSpeed", Mathf.Abs(playerBody.velocity.x));
            return;
        }
        // END OF PC BINDING


        if (!gameMenuManager.CanPlayerMove() || Input.touchCount <= 0) { StopMoving(); return; }
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

        animator.SetFloat("playerSpeed", Mathf.Abs(playerBody.velocity.x));
        
    }

    private void StartMoving()
    {
        playerIsMoving = true;
    }

    private void StopMoving()
    {
        playerMovementDirection = 0;
        playerIsMoving = false;
        //controller2D.Move(0, false, false);
        playerBody.velocity = Vector3.zero;
    }

    void FixedUpdate() {

        if (!playerIsMoving) { return; }

        float distanceToTarget = playerTargetPosition.x - transform.position.x;
        float distanceToNextPosition = Mathf.Abs(playerMovementSpeed * Time.fixedDeltaTime);
            
        float EPSILON = 0.001f;

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

        if (playerMovementDirection > 0 && !m_FacingRight || playerMovementDirection < 0 && m_FacingRight)
        {
            Flip();
        }

        
        Vector3 targetVelocity = new Vector2(playerMovementDirection*distanceToNextPosition*10, playerBody.velocity.y);
        
        playerBody.velocity = Vector3.SmoothDamp(playerBody.velocity, targetVelocity, ref m_Velocity , 0f);
       // controller2D.Move(playerMovementDirection * distanceToNextPosition, false, false);

    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
