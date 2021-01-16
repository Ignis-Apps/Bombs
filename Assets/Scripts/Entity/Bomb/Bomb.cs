using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float startSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;

   
    public float shakeAngle;
    public Rigidbody2D bombBody;

    public GameObject explosionPrefab;
    public GameObject scoreOrbPrefab;
    public GameObject coinPrefab;

    private float angularAcceleration = 30f;

    private float ScorePointDropRate_Percent = 25;
    private float CoinDropRate_Percent = 15;

    // Start is called before the first frame update
    void Start()
    {
        Animator anim = GetComponent<Animator>();
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
        anim.Play(state.fullPathHash, -1, Random.Range(0f, 1f));

        bombBody.velocity = transform.up * - startSpeed;
        bombBody.angularVelocity = angularAcceleration;
    }

    // Update is called once per frame
    void Update()
    {

     
        if(bombBody.rotation >= shakeAngle)
        {
            bombBody.angularVelocity = -angularAcceleration;
        }
        if(bombBody.rotation <= -shakeAngle)
        {
            bombBody.angularVelocity = +angularAcceleration;
        }


        if(bombBody.velocity.y < -maxSpeed)
        {
            bombBody.velocity = new Vector2(0,bombBody.velocity.y - acceleration);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(explosionPrefab, bombBody.transform.position, bombBody.transform.rotation);
        
        if(Random.Range(0,100) < ScorePointDropRate_Percent)
        {
            
            for (int i = Random.Range(1,5); i>0; i--)
            {
                Instantiate(scoreOrbPrefab, bombBody.transform.position, bombBody.transform.rotation);
            }
            
        }

        if (Random.Range(0, 100) < CoinDropRate_Percent)
        {

            for (int i = Random.Range(1, 3); i > 0; i--)
            {
                Instantiate(coinPrefab, bombBody.transform.position, bombBody.transform.rotation);
            }

        }

        if (!collision.gameObject.name.Contains("Player"))
        {
            GameManager.GetInstance().OnBombDodged();
        }

        Destroy(gameObject);
    }
}
