using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    private float startSpeed;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float acceleration;



    public float shakeAngle;
    public Rigidbody2D bombBody;

    public GameObject explosionPrefab;
    public GameObject scoreOrbPrefab;
    public GameObject coinPrefab;

    private float angularAcceleration = 30f;
    private float ScorePointDropRate_Percent = 25;
    private float CoinDropRate_Percent = 20;

    // Start is called before the first frame update
    void Start()
    {
        //Animator anim = GetComponent<Animator>();
        //AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
        //anim.Play(state.fullPathHash, -1, Random.Range(0f, 1f));

        bombBody.velocity = transform.up * -startSpeed;
       // bombBody.angularVelocity = angularAcceleration;
    }

    // Update is called once per frame
    void Update()
    {
        bombBody.velocity *= 1 + (acceleration * Time.deltaTime);
        bombBody.velocity = -transform.up * bombBody.velocity.magnitude;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject explosion = Instantiate(explosionPrefab, bombBody.transform.position, bombBody.transform.rotation);
        explosion.transform.localScale = new Vector2(0.3f, 0.3f);

        if (Random.Range(0, 100) < ScorePointDropRate_Percent)
        {

            for (int i = Random.Range(1, 5); i > 0; i--)
            {
                Instantiate(scoreOrbPrefab, bombBody.transform.position, bombBody.transform.rotation);
            }

        }

        if (Random.Range(0, 100) < CoinDropRate_Percent)
        {

            for (int i = Random.Range(1, 1); i > 0; i--)
            {
                Instantiate(coinPrefab, bombBody.transform.position, bombBody.transform.rotation);
            }

        }

        if (!collision.gameObject.name.Contains("Player"))
        {
            GameManager.GetInstance().addDodgedBomb();
        }

        transform.position = new Vector2(0, -1000);
        //bombBody.bodyType = RigidbodyType2D.Static;
        Destroy(this.gameObject,0.5f);
    }
}
