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
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;// * (1f/transform.localScale.x);        
    }


    private void Update()
    {
        
        Vector3 deltaPosition = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaPosition.x * effectStrength.x, deltaPosition.y * effectStrength.y, 0f);
        lastCameraPosition = cameraTransform.position;

        
        if (cameraTransform.position.x - transform.position.x > spriteWidth )
        {
            transform.position += new Vector3(spriteWidth*3, 0, 0);
        }

        if (cameraTransform.position.x - transform.position.x < -spriteWidth )
        {
            transform.position -= new Vector3(spriteWidth*3, 0, 0);
        }

        

        //Debug.Log(spriteWidth);

        /*
        float temp = (cameraTransform.position.x * (1f - effectStrength.x));
        float dist = (cameraTransform.position.x * effectStrength.x);

        transform.position = new Vector3(startPosition + dist, transform.position.y, transform.position.z);

        if (temp > startPosition + spriteWidth) startPosition += spriteWidth;
        else if (temp < startPosition - spriteWidth) startPosition -= spriteWidth;
        */

    }
}
