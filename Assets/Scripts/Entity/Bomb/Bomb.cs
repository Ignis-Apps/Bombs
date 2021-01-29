using UnityEngine;

public class Bomb : MonoBehaviour
{

    [SerializeField] private float startSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float shakeAngle;
    
    [SerializeField] private LootTableSettings lootTableSettings;
    
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject scoreOrbPrefab;
    [SerializeField] private GameObject coinPrefab;
    
    private Rigidbody2D bombRigidBody;
    private float angularAcceleration = 30f;

    // Start is called before the first frame update
    public void Start()
    {
        bombRigidBody = GetComponent<Rigidbody2D>();

        Animator animationControlelr = GetComponent<Animator>();
        AnimatorStateInfo currentState = animationControlelr.GetCurrentAnimatorStateInfo(0);
        animationControlelr.Play(currentState.fullPathHash, -1, Random.Range(0f, 1f));

        GameManager gameManager = GameManager.GetInstance();
        
        float waveStartSpeed = gameManager.CurrentWave.GetDefaultBombInitialSpeed(gameManager.CurrentWaveProgress);

        bombRigidBody.velocity = transform.up * - waveStartSpeed;
        bombRigidBody.angularVelocity = angularAcceleration;
    }

    // Update is called once per frame
    public void Update()
    {
  
        if(bombRigidBody.rotation >= shakeAngle)
        {
            bombRigidBody.angularVelocity = -angularAcceleration;
        }
        if(bombRigidBody.rotation <= -shakeAngle)
        {
            bombRigidBody.angularVelocity = +angularAcceleration;
        }
        if(bombRigidBody.velocity.y < -maxSpeed) 
        {
            bombRigidBody.velocity = new Vector2(0,bombRigidBody.velocity.y - acceleration);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(explosionPrefab, bombRigidBody.transform.position, bombRigidBody.transform.rotation);

        SpawnPrefabs(coinPrefab, lootTableSettings.GetRandomCoinAmount());
        SpawnPrefabs(scoreOrbPrefab, lootTableSettings.GetRandomScoreAmount());
        SpawnPrefabs(coinPrefab, lootTableSettings.GetRandomSpecialCoinAmount());

        if (!collision.gameObject.name.Contains("Player"))
        {
            GameManager.GetInstance().OnBombDodged();
        }

        Destroy(gameObject);
    }

    private void SpawnPrefabs(GameObject prefab, int amount)
    {
        for(int i=0; i<amount; i++)
        {
            Instantiate(prefab, bombRigidBody.transform.position, bombRigidBody.transform.rotation);
        }
    }
}
