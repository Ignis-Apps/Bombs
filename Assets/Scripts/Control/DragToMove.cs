using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragToMove : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private MovementController movementController;
    [SerializeField] private GameObject targetPoint;

    [SerializeField] private float timeReduction;
    [SerializeField] private float dragScale;
    [SerializeField] private bool inverted;

    private LineRenderer lineRenderer;
    
    private bool dragStarted;

    private Vector2 dragStartPosition;

    // Start is called before the first frame update
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
     
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            dragStarted = true;
            DragStart();
        }

        if (!dragStarted) { return; }

        if (Input.GetMouseButton(0))
        {
            DragDragging();
        }
        else
        {
            dragStarted = false;
            DragRelease();
        }

    }

    void DragStart()
    {
        dragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, dragStartPosition);
        
        Time.timeScale = timeReduction;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
    }

    void DragDragging()
    {
        Vector2 dragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(1, dragPosition);

        // Preview TargetPoint
        Vector2 direction = dragStartPosition - dragPosition;
        direction *= dragScale;

        if (inverted)
        {
            direction *= -1f;
        }

        Vector2 pos = (Vector2)player.transform.position + direction;
        pos.y = targetPoint.transform.position.y;
        targetPoint.transform.position = pos; 
    }

    void DragRelease()
    {
        lineRenderer.positionCount = 0;
        movementController.SetTargetPosition(targetPoint.transform.position);
        Time.timeScale = 1f;

    }
}
