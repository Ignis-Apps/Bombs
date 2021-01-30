using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStick : MonoBehaviour
{
    // If true, the controller is placed where the finger touches the screen
    public bool dynamicControllerPosition;  
    public bool allowVertiaclAxis;
    
    public float size = 2f;
    public float inputThreshold;
    
    [SerializeField] MovementController controllable;
    [SerializeField] Transform controllerBase;
    [SerializeField] Transform controllerStick;
    [SerializeField] AnimationCurve sensetivityCurve;

    private Vector2 controllerPosition;
    private Vector2 stickPosition;
    private bool readInput;

    // Update is called once per frame
    void Update()
    {
        if(controllable == null) { return; }

        if (Input.GetMouseButtonDown(0)) { OnMouseDown(); }
        
        if (!readInput) { return; }

        if (Input.GetMouseButton(0)) { OnMouseDragged(); }
        else{ OnMouseUp(); }

        controllerBase.transform.localScale = new Vector3(size, controllerBase.transform.localScale.y, 1f);
       
    }

    private void OnMouseDown()
    {
        readInput = true;
        
        // Ensure that all components are visible
        controllerBase.GetComponent<SpriteRenderer>().enabled = true;
        controllerStick.GetComponent<SpriteRenderer>().enabled = true;

        // Set position of joystick if position is not fixed
        if (dynamicControllerPosition)
        {
            controllerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            controllerBase.transform.position = controllerPosition;
        }
      
    }

    private void OnMouseDragged(){
        
        stickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        readInput = false;

        // Stop the player
        controllable.Stop();

        // Reset the stick to its origin
        controllerStick.transform.position = controllerBase.position;

        // Hide components if joystick position is not fixed
        if (dynamicControllerPosition)
        {
            controllerBase.GetComponent<SpriteRenderer>().enabled = false;
            controllerStick.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void FixedUpdate()
    {
        if (readInput)
        {
            Vector2 direction;
            if (allowVertiaclAxis)
            {
               // direction = Vector2.ClampMagnitude(stickPosition - controllerPosition, 1.0f);
                direction = Vector2.ClampMagnitude(stickPosition - controllerPosition, size/2f);
            }
            else
            {
                direction = new Vector2(Mathf.Clamp(stickPosition.x - controllerPosition.x, -size/2f, size/2f), 0f);
            }


            controllerStick.transform.position = new Vector2(controllerBase.position.x + direction.x, controllerBase.position.y + direction.y);
            
            direction *= 2f / size;
            direction.x = sensetivityCurve.Evaluate(direction.x);
            direction.y = sensetivityCurve.Evaluate(direction.y);

            if (direction.magnitude < inputThreshold)
            {
                return;
            }
     
            direction.y = 0;


            controllable.Move(direction); 
        }
    }
}
