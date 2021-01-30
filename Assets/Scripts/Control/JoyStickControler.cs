using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickControler : MonoBehaviour
{
    [SerializeField] JoyStickBase onScreenController;
    [SerializeField] MovementController playerMovementController;
 
    // Update is called once per frame
    void Update()
    {
        Vector2 movementDirection = new Vector2(onScreenController.GetHorizontal(), onScreenController.GetVertical());
        playerMovementController.Move(movementDirection);
    }
}
