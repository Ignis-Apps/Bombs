using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBomb : MonoBehaviour
{
    [SerializeField] private float startSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;

    public float shakeAngle;
    
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject scoreOrbPrefab;
    [SerializeField] private GameObject coinPrefab;

    [SerializeField] private LootTableSettings lootTableSettings;
 
    private Rigidbody2D bombRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        bombRigidBody = GetComponent<Rigidbody2D>();

        GameManager gameManager = GameManager.GetInstance();
        float waveStartSpeed = gameManager.CurrentWave.GetHomingBombInitialSpeed(gameManager.CurrentWaveProgress);
        bombRigidBody.velocity = transform.up * -waveStartSpeed;
       
    }

    // Update is called once per frame
    void Update()
    {
        bombRigidBody.velocity *= 1 + (acceleration * Time.deltaTime);
        bombRigidBody.velocity = -transform.up * bombRigidBody.velocity.magnitude;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject explosion = Instantiate(explosionPrefab, bombRigidBody.transform.position, bombRigidBody.transform.rotation);
        explosion.transform.localScale = new Vector2(0.3f, 0.3f);

        SpawnPrefabs(coinPrefab, lootTableSettings.GetRandomCoinAmount());
        SpawnPrefabs(scoreOrbPrefab, lootTableSettings.GetRandomScoreAmount());
        SpawnPrefabs(coinPrefab, lootTableSettings.GetRandomSpecialCoinAmount());

        if (!collision.gameObject.name.Contains("Player"))
        {
            GameManager.GetInstance().OnBombDodged();
        }

        transform.position = new Vector2(0, -1000);        
        Destroy(this.gameObject,0.5f);
    }

    private void SpawnPrefabs(GameObject prefab, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(prefab, bombRigidBody.transform.position, bombRigidBody.transform.rotation);
        }
    }
}
