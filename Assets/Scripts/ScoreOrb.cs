using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreOrb : MonoBehaviour
{
    public Rigidbody2D body;
    public float maxLifeTime;
    public float HoverAmplitude;
    public float HoverFrequency;

    [SerializeField]
    private float initialMovementAngleMin;
    [SerializeField]
    private float initialMovementAngleMax;
    [SerializeField]
    private float initialMovementSpeedMin;
    [SerializeField]
    private float initialMovementSpeedMax;

    private float CurrentAmpPos = Mathf.PI * 1.5f;
    private float lifetime = 0f;
    private bool isOnGround;
    private Vector2 HoverOrigin;

    // Start is called before the first frame update
    private void Start()
    {
        // HoverOrigin = body.transform.position;
        float startAngle = Random.Range(initialMovementAngleMin, initialMovementAngleMax);
        startAngle *= Mathf.PI / 180f;
        Vector2 startDir = new Vector2(Mathf.Cos(startAngle), Mathf.Sin(startAngle));
        float startSpeed = Random.Range(initialMovementSpeedMin, initialMovementSpeedMax);
        body.velocity = startDir * startSpeed;

    }
    private void FixedUpdate()
    {

        if (!isOnGround || body.velocity.magnitude>0) { return; }

        CurrentAmpPos += Time.deltaTime * ( HoverFrequency / 1 );
        lifetime += Time.deltaTime;
        
        if(lifetime > maxLifeTime)
        {
            Destroy(gameObject);
        }

        float HoverHeight = Mathf.Sin(CurrentAmpPos) * HoverAmplitude;
        body.position = HoverOrigin + new Vector2(0, HoverHeight);

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") && !isOnGround)
        {
            body.bodyType = RigidbodyType2D.Kinematic;
            body.velocity = new Vector2(0, 0);
            HoverOrigin = body.transform.position;
            isOnGround = true;
        }
        
    }
}
