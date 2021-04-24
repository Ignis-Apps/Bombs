using UnityEngine;

namespace Assets.Scripts.Entity.Bomb
{
    public abstract class Bomb : MonoBehaviour
    {

        [SerializeField] private GameObject explosionPrefab;
        [SerializeField] private GameObject scoreOrbPrefab;
        [SerializeField] private GameObject coinPrefab;
        [SerializeField] private GameObject crystalPrefab;

        [SerializeField] private float explosionSize;

        // [SerializeField] private float startSpeed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float acceleration;

        [SerializeField] private float angularAcceleration;     // Acceleration of the rotation on the z-axis
        [SerializeField] private float shakeAngle;              // Angle at which the acceleration pulls in the opposite direction

        [SerializeField] private LootTableSettings lootTableSettings;

        protected GameManager gameManager;
        
        protected Rigidbody2D bombBody;
        

        private void Awake()
        {
            gameManager = GameManager.GetInstance();
        }

        protected virtual void Start()
        {
            // Load Components
            Animator animController = GetComponent<Animator>();
            bombBody = GetComponent<Rigidbody2D>();
            
            // Choose random animation start
            AnimatorStateInfo currentState = animController.GetCurrentAnimatorStateInfo(0);
            animController.Play(currentState.fullPathHash, -1, Random.Range(0f, 1f));

            // Apply start speed
            float waveStartSpeed = GetStartSpeed();
            bombBody.velocity = transform.up * -waveStartSpeed;
            bombBody.angularVelocity = angularAcceleration;

        }



        protected virtual void FixedUpdate()
        {

            // Turn left
            if (bombBody.rotation >= shakeAngle)
            {
                bombBody.angularVelocity = -angularAcceleration;
            }

            // Turn right
            if (bombBody.rotation <= -shakeAngle)
            {
                bombBody.angularVelocity = +angularAcceleration;
            }

            // Accelerate to max speed
            if (bombBody.velocity.magnitude <= maxSpeed)
            {
                bombBody.velocity *= 1 + (acceleration * Time.deltaTime);
            }

        }

        public abstract float GetStartSpeed();


        private void OnTriggerEnter2D(Collider2D collision)
        {

            GameObject explosion = Instantiate(explosionPrefab, bombBody.transform.position, bombBody.transform.rotation);
            explosion.transform.localScale = new Vector2(explosionSize, explosionSize);

            if (collision.tag.Contains("Ground"))
            {
                Vector3 explositionPoistion = explosion.transform.position;
                float explosionHeight = explosion.GetComponent<SpriteRenderer>().bounds.size.y * explosionSize;
                explositionPoistion.y = gameManager.GroundTransform.position.y + (explosionHeight / 2f);
                explosion.transform.position = explositionPoistion;
            }

            SpawnPrefabs(coinPrefab, lootTableSettings.GetRandomCoinAmount());
            //SpawnPrefabs(scoreOrbPrefab, lootTableSettings.GetRandomScoreAmount());           
            SpawnPrefabs(crystalPrefab, lootTableSettings.GetRandomSpecialCoinAmount());

            if (!collision.gameObject.name.Contains("Player"))
            {
                GameManager.GetInstance().OnBombDodged();
            }

            Destroy(gameObject);
        }

        private void SpawnPrefabs(GameObject prefab, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Instantiate(prefab, bombBody.transform.position, bombBody.transform.rotation);
            }
        }
    }
}
