using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxEffect : MonoBehaviour
{
    [SerializeField] Vector2 effectStrength;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        Vector3 deltaPosition = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaPosition.x * effectStrength.x, deltaPosition.y * effectStrength.y, 0f);
        lastCameraPosition = cameraTransform.position;
    }
}
