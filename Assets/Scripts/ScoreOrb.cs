using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreOrb : MonoBehaviour
{
    public Rigidbody2D body;
    public float maxLifeTime;
    public float HoverAmplitude;
    public float HoverFrequency;


    private float CurrentAmpPos = Mathf.PI * 1.5f;
    private float lifetime = 0f;
    private Vector2 HoverOrigin;

    // Start is called before the first frame update
    private void Start()
    {
        HoverOrigin = body.transform.position;
    }
    private void FixedUpdate()
    {
      
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
     
    }
}
