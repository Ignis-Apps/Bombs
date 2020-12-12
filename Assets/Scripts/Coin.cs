using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public Animator coinAnimator;
    public Rigidbody2D coinBody;
    public float startSpeed;
 

    // Start is called before the first frame update
    void Start()
    {
        coinBody.velocity = transform.up * -startSpeed;
    }

    // Update is called once per frame  
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        coinBody.velocity = new Vector2(0, 0);
    }
}
