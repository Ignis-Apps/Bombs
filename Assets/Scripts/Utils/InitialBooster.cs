using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialBooster : MonoBehaviour
{
    [SerializeField] private float initialMovementAngleMin;
    [SerializeField] private float initialMovementAngleMax;
    [SerializeField] private float initialMovementSpeedMin;
    [SerializeField] private float initialMovementSpeedMax;
    
    // Start is called before the first frame update
    void Start()
    {

        Rigidbody2D body = GetComponent<Rigidbody2D>();
        float startAngle = Random.Range(initialMovementAngleMin, initialMovementAngleMax);
        startAngle *= Mathf.PI / 180f;
        Vector2 startDir = new Vector2(Mathf.Cos(startAngle), Mathf.Sin(startAngle));
        float startSpeed = Random.Range(initialMovementSpeedMin, initialMovementSpeedMax);
        body.velocity = startDir * startSpeed;

    }


}
