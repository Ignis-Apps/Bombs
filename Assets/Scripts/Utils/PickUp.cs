﻿using System.Collections;
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
            gameObject.layer = LayerMask.NameToLayer("NoCollision");
            rigidBody.bodyType = RigidbodyType2D.Static;

            StartCoroutine(Pickup(collision.gameObject.transform, timeToPickup));

        }
    }

    IEnumerator Pickup(Transform target, float time)
    {

        float elapsed = 0f;
        
        Vector3 startScale = transform.localScale;        

        while(elapsed < time)
        {
            elapsed += Time.deltaTime;
            float p = elapsed / time;
            transform.position = Vector3.Lerp(transform.position, target.position, p);
            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, p);
            yield return null;
        }

        Destroy(gameObject);

    }
}
