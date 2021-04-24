using UnityEngine;

public class ScoreOrb : MonoBehaviour
{
    public Rigidbody2D body;
    
    public float HoverAmplitude;
    public float HoverFrequency;

    private float CurrentAmpPos = Mathf.PI * 1.5f;
    
    private bool isOnGround;
    private Vector2 HoverOrigin;

    private void FixedUpdate()
    {
        if (!isOnGround || body.velocity.magnitude>0) { return; }
        CurrentAmpPos += Time.deltaTime * ( HoverFrequency / 1 );
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
