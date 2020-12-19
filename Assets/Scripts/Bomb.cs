using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public float startSpeed;
    public float maxSpeed;
    public float acceleration;

    public float shakeAngle;
    private float angularAcceleration = 30f;
    public Rigidbody2D bombBody;

    public GameObject explosionPrefab;
    public GameObject scoreOrbPrefab;

    private float ScorePointDropRate_Percent = 25;

    // Start is called before the first frame update
    void Start()
    {
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
            Instantiate(scoreOrbPrefab, bombBody.transform.position, bombBody.transform.rotation);
        }

        if (!collision.gameObject.name.Contains("Player"))
        {
            GameManager.GetInstance().addDodgedBomb();
        }

        Destroy(gameObject);
    }
}
