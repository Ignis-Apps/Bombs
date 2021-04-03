using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollower : MonoBehaviour
{
    [SerializeField] private GameObject target;

    [SerializeField] private bool followXAxis;
    [SerializeField] private bool followYAxis;

    private Transform targetTransform;

    private void Start()
    {
        targetTransform = target.transform;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        Vector3 nextPosition = transform.position;

        if (followXAxis)
        {
            nextPosition.x = targetTransform.position.x;
        }

        if (followYAxis)
        {
            nextPosition.y = targetTransform.position.y;
        }

        transform.position = nextPosition;

    }
}
