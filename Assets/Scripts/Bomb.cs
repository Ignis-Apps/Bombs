using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public float startSpeed;
    public float maxSpeed;
    public float acceleration;

    public float shakeAngle;
    public Rigidbody2D bombBody;
    public Transform[] spawnPoints;
    private float angularAcceleration = 30f;
    public GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        bombBody.velocity = transform.up * - startSpeed;
        bombBody.position = spawnPoints[0].position;
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
        Debug.Log(collision.name);
        //bombBody.velocity = new Vector2(0, 0);        
        bombBody.position = spawnPoints[Random.Range(0,spawnPoints.Length)].position;
        bombBody.velocity = new Vector2(0f,-startSpeed);
        Instantiate(explosionPrefab, bombBody.transform.position, bombBody.transform.rotation);
    }
}
