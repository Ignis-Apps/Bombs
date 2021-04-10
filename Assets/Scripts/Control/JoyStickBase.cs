using Assets.Scripts.Control;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JoyStickBase : MonoBehaviour
{
    [SerializeField] private Transform handle;

    [Header("Current Joystick Values (Read Only)")]
    [SerializeField] private float horizontal = 0f;
    [SerializeField] private float vertical = 0f;

    [Header("Input Area Restrictions")]
    [SerializeField] private Transform upperBorder;
    
    [Header("Joystick Input Settings")]
    [SerializeField] private float threshold;
    [SerializeField] private float sensitivity;
    
    [Header("Joystick Output Settings")]
    [SerializeField] private bool disableVerticalOutput;

    [Header("Joystick Visuals")]
    [SerializeField] [Range(0,1)] private float maxHandlePosition;
    [SerializeField] private bool hideIfNotInUse;
    
    
    private float handleSize;
    private float baseRadius;
    private float handleRadius;
    private bool readInput;

    private List<SpriteRenderer> spriteRenderers;

    private ScreenManager screenManager;
    private ControllerState controllerState;
    
    
    private void Awake()
    {
        screenManager = ScreenManager.GetInstance();
        controllerState = ControllerState.GetInstance();
        
        baseRadius = transform.lossyScale.x / 2f;
        handleRadius = handle.transform.lossyScale.x / 2f;
        handleSize = transform.localScale.x / 2f;
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>().ToList();
        
        SetVisibility(false);
    }

    public void Update()
    {
      
        if (Input.GetMouseButtonDown(0) && screenManager.CanPlayerMove()) { OnTouchDown(); }

        if (!readInput) { return; }

        if (Input.GetMouseButton(0)) { OnTouchMove(); }
        else { OnTouchUp(); }

        controllerState.stickPositionX = GetHorizontal();
        controllerState.stickPositionY = GetVertical();
        
        controllerState.stickPositionXRaw = horizontal;
        controllerState.stickPositionYRaw = vertical;

    }


    private void OnTouchDown()
    {

        // Check if touch is within the valid input zone
        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if( upperBorder!=null && touchPosition.y > upperBorder.position.y ){ return; }

        
        SetVisibility(true);
        
        
        readInput = true;
    }

    private void OnTouchMove()
    {
        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 handleDirection = touchPosition - (Vector2) transform.position;
        handleDirection *= sensitivity;
        
        handleDirection = Vector2.ClampMagnitude(handleDirection, baseRadius);
        
        Vector2 handleDirectionNom = handleDirection.normalized;
        horizontal = handleDirection.x * (1/baseRadius);
        vertical = handleDirection.y * (1/baseRadius) ;
        
        //handle.position = transform.position + (Vector3) (handleDirection * handleSize * handleScale);
        handle.position = transform.position + (Vector3) (handleDirection * maxHandlePosition);

    }

    private void OnTouchUp()
    {
        if (!readInput) { return; }

        if (hideIfNotInUse || !screenManager.CanPlayerMove())
        {
            SetVisibility(false);
        }

        readInput = false;
        handle.localPosition = Vector3.zero;
        horizontal = 0;
        vertical = 0;
    }

 
    private void SetVisibility(bool visible)
    {
        spriteRenderers.ForEach(renderer => renderer.enabled = visible);
    }

    public float GetHorizontal()
    {
        return Mathf.Abs(horizontal) >= threshold ? horizontal : 0;
    }

    public float GetVertical()
    {
        if (disableVerticalOutput)
        {
            return 0;
        }

        return ( Mathf.Abs(vertical) >= threshold ) ? vertical : 0;
    }
}
