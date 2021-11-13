using Assets.Scriptable;
using Assets.Scripts.Game.Session;
using System.Collections;
using UnityEngine;

public class Crate : MonoBehaviour
{

    [SerializeField] private LayerMask whatIsGround;  
    [SerializeField] private Transform groundCheck;

    [SerializeField] private GameObject parachute;
    [SerializeField] private GameObject crate;
    [SerializeField] private GameObject progressIndicator;

    [SerializeField] private float dropVelocity;
    [SerializeField] private AnimationClip collapseAnimation;
    [SerializeField] private AnimationClip openCrateAnimation;

    [SerializeField] private float requiredOpeningTime;
    private float openingProgress;

    [SerializeField] private CrateSettings crateSettings;
    
    private Animator animator;
    private Rigidbody2D body;
    private BoxCollider2D boxColider;
    private Transform crateTransform;

    private GameManager gameManager;

    private bool hasLanded;
    private bool isPlayerNear;

    private bool hasFixedDrop;
    private GameObject fixedDropPrefab;

    // Start is called before the first frame update
    void Start()
    {        
        animator   = GetComponent<Animator>();
        body       = GetComponent<Rigidbody2D>();
        boxColider = GetComponent<BoxCollider2D>();
        crateTransform = GetComponent<Transform>();

        gameManager = GameManager.GetInstance();

        body.velocity = new Vector2(0, -dropVelocity);
        progressIndicator.SetActive(false);
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
                    if (GameSessionEventHandler.crateLandedDelegate != null)
                    {
                        GameSessionEventHandler.crateLandedDelegate();
                    }

                    requiredOpeningTime = Mathf.Max(requiredOpeningTime - (gameManager.session.progressStats.SurvivedWaves * .2f), .2f);

                }
            }
        }
      
        if (isPlayerNear && gameManager.getPlayer().GetMovementController().GetVirtualSpeed() < .3f)
        {
            openingProgress += Time.deltaTime;

            progressIndicator.transform.localScale = new Vector3(1 - (openingProgress / requiredOpeningTime), 1, 1);

            if(openingProgress >= requiredOpeningTime) {
                isPlayerNear = false;                            
                StartCoroutine(OpenCrate());
            }

        }
     
    }

    IEnumerator OpenCrate()
    {   
        animator.Play(openCrateAnimation.name);

        gameManager.session.playerStats.IsNearCrate = false;
        yield return new WaitForSeconds(openCrateAnimation.length);

        if (GameSessionEventHandler.crateOpenedDelegate != null)
            GameSessionEventHandler.crateOpenedDelegate();
        
        GameObject drop;
        
        if(hasFixedDrop)
            drop = Instantiate(fixedDropPrefab, transform.position, transform.rotation);
        else    
            drop = Instantiate(crateSettings.GetCrateDrop(), transform.position, transform.rotation);

        SoundManager.PlaySound(SoundManager.Sound.POWERUP_RELEASED);
        drop.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0f, 250f));


        Destroy(this.gameObject);

    }

    private void DetatchParachute()
    {
        animator.Play(collapseAnimation.name);
        
        Destroy(parachute,collapseAnimation.length);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // Ensures that the player can walk into the crate
        if (collision.CompareTag("Ground"))
        {
            Rigidbody2D body = GetComponent<Rigidbody2D>();



            // transform.position = new Vector3(transform.position.x, GameManager.GetInstance().GroundTransform.position.y + GetComponent<SpriteRenderer>().bounds.size.y / 2f, transform.position.z);
            transform.position = new Vector3(transform.position.x, GameManager.GetInstance().GroundTransform.position.y + GetComponent<SpriteRenderer>().bounds.size.y / 2f, transform.position.z);



            boxColider.isTrigger = true;
            body.velocity = Vector2.zero;
            body.bodyType = RigidbodyType2D.Static;
        }

        if (collision.CompareTag("Player"))
        {
            isPlayerNear = true;
            gameManager.session.playerStats.IsNearCrate = true;
            progressIndicator.SetActive(true);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           // progressIndicator.SetActive(false);
            isPlayerNear = false;
            gameManager.session.playerStats.IsNearCrate = false;
        }

    }

    public void SetCrateDrop(GameObject prefab)
    {
        hasFixedDrop = true;
        fixedDropPrefab = prefab;
    }
}
