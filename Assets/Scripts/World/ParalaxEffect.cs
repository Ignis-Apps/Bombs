using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxEffect : MonoBehaviour
{
    [SerializeField] Vector2 effectStrength;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    private float startPosition;
    private float spriteWidth;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }

    private void Start()
    {
        startPosition = transform.position.x;
        
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            spriteWidth = renderer.bounds.size.x;
        }
    }


    private void Update()
    {
        
        Vector3 deltaPosition = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaPosition.x * effectStrength.x, deltaPosition.y * effectStrength.y, 0f);
        lastCameraPosition = cameraTransform.position;

        // UNCOMMENT TO ENABLE INFINITE WORLD

        
        /*
        if (cameraTransform.position.x - transform.position.x > spriteWidth )
        {
            transform.position += new Vector3(spriteWidth*3, 0, 0);
        }

        if (cameraTransform.position.x - transform.position.x < -spriteWidth )
        {
            transform.position -= new Vector3(spriteWidth*3, 0, 0);
        }
        */
       

    }
}
