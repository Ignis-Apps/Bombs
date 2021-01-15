using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{

    [SerializeField] private LayerMask whatIsGround;  
    [SerializeField] private Transform groundCheck;

    [SerializeField] private GameObject parachute;
    [SerializeField] private GameObject progressIndicator;

    [SerializeField] private float dropVelocity;
    [SerializeField] private AnimationClip collapseAnimation;

    [SerializeField] private float requiredOpeningTime;
    private float openingProgress;
    
    private Animator animator;
    private Rigidbody2D body;
    private BoxCollider2D collider2D;

    private bool hasLanded;
    private bool isPlayerNear;

    // Start is called before the first frame update
    void Start()
    {        
        animator   = GetComponent<Animator>();
        body       = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<BoxCollider2D>();

        body.velocity = new Vector2(0, -dropVelocity);
        progressIndicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!hasLanded) 
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, .2f, whatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    hasLanded = true;
                    DetatchParachute();
                }
            }
        }

        if (isPlayerNear)
        {
            openingProgress += Time.deltaTime;

            progressIndicator.transform.localScale = new Vector3(1 - (openingProgress / requiredOpeningTime), 1, 1);

            if(openingProgress >= requiredOpeningTime) {
                isPlayerNear = false;
                OpenCrate();
            }

        }

     
    }

    private void OpenCrate()
    {
        Destroy(this.gameObject);
    }

    private void DetatchParachute()
    {
        animator.Play(collapseAnimation.name);
        Destroy(parachute,2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // Ensures that the player can walk into the crate
        if (collision.CompareTag("Ground"))
        {
            collider2D.isTrigger = true;
            body.velocity = Vector2.zero;
            body.bodyType = RigidbodyType2D.Static;
        }

        if (collision.CompareTag("Player"))
        {
            isPlayerNear = true;
            progressIndicator.SetActive(true);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           // progressIndicator.SetActive(false);
            isPlayerNear = false;
        }

    }
}
