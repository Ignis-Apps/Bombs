using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PickUp : MonoBehaviour
{
    [SerializeField]
    private float timeToPickup;

    private Rigidbody2D rigidBody;
    private Vector3 originalScale;
    private float timeSincePickup;
    private bool isPickedUp;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }


    void Update()
    {

        if (isPickedUp)
        {
            timeSincePickup += Time.deltaTime;
            gameObject.transform.localScale = originalScale * ((timeToPickup - timeSincePickup) / timeToPickup);

            if (timeSincePickup > timeToPickup)
            {
                Destroy(gameObject);
            }

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Calculate the velocity to arive at destination in time
            Vector2 dir = collision.gameObject.transform.position - transform.position;                            
            dir /= timeToPickup;
            
            // Move object to an non colission layer
            gameObject.layer = LayerMask.NameToLayer("NoCollision");

            // Apply velocity and take controll over the physics
            rigidBody.bodyType = RigidbodyType2D.Kinematic;
            rigidBody.velocity = dir;

            // Mark as picked up and prepare downscale animation
            originalScale = transform.localScale;
            isPickedUp = true;

        }
    }
}
