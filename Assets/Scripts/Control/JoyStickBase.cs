using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickBase : MonoBehaviour
{
    private bool readInput;

    private float horizontal = 0f;
    private float vertical = 0f;

    private float handleSize;

    [SerializeField] private Transform handle;
    [SerializeField] private float threshold;
    [SerializeField] private float handleScale;
    [SerializeField] private float sensitivity;
    [SerializeField] private bool disableVerticalOutput;

    private void Awake()
    {
        handleSize = transform.localScale.x / 2f;
    }

    private void OnMouseDown()
    {
        readInput = true;
    }

    private void OnMouseDrag()
    {
        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 handleDirection = touchPosition - (Vector2) transform.position;
        handleDirection *= sensitivity;
        
        handleDirection = Vector2.ClampMagnitude(handleDirection, 1.0f);
        //handleDirection.Normalize();
        horizontal = handleDirection.x ;
        vertical = handleDirection.y ;
        
        handle.position = transform.position + (Vector3) (handleDirection * handleSize * handleScale);

    }

    private void OnMouseUp()
    {
        if (!readInput) { return; }
        readInput = false;
        handle.localPosition = Vector3.zero;
        horizontal = 0;
        vertical = 0;
    }

 
    public float GetHorizontal()
    {
        return Mathf.Abs(horizontal) >= threshold ? horizontal : 0;
    }

    public float GetVertical()
    {
        return Mathf.Abs(vertical) <= threshold ||disableVerticalOutput ? 0 : vertical;
    }
}
