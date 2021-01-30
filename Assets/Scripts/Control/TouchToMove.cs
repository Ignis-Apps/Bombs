using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTouch : MonoBehaviour
{
    [SerializeField] private MovementController movementController;
    [SerializeField] private float InputScale;
    [SerializeField] private GameObject targetingPoint;

    Vector2 bottomLeft;
    Vector2 topRight;
    private void Awake()
    {
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    void Update()
    { 
        if (Input.GetMouseButton(0))
        {                
            Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            targetPosition *= InputScale;
            targetPosition.x = Mathf.Clamp(targetPosition.x, bottomLeft.x, topRight.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, bottomLeft.y, topRight.y);

            movementController.SetTargetPosition(targetPosition);

            if (targetingPoint != null)
            {
                targetingPoint.transform.position = new Vector2(targetPosition.x, targetingPoint.transform.position.y);
            }
        }
        else
        {
            movementController.ClearTargetPosition();
            movementController.Stop();
        }

    }
}
