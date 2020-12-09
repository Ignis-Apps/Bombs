﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller2D;
    public Animator animator;

    public float movementSpeed = 20f;
    float moveDir = 0.5f;

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount <= 0)
        {
            return;
        }

        Touch touch = Input.GetTouch(0);

        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(touch.position);

        float distance = transform.position.x - targetPosition.x;

        float threshold = 0.1f;

        if(distance < -threshold)
        {
            moveDir = movementSpeed;
        }else if (distance > threshold)
        {
            moveDir = -movementSpeed;
        }
        else
        {
            moveDir = 0;
        }
        /*
        if(targetPosition.x <= transform.position.x)
        {
            moveDir = -movementSpeed;
        }
        else
        {
            moveDir = movementSpeed;
        }
        
        if(touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
        {
            moveDir = movementSpeed;
            if (touch.position.x < Screen.width/2)
                moveDir *= -1;
        }
        */
        if (touch.phase == TouchPhase.Ended)
        {
            moveDir = 0;
        }
        
        animator.SetFloat("playerSpeed", Mathf.Abs(moveDir));        

    }

    void FixedUpdate() {

        controller2D.Move(moveDir * Time.fixedDeltaTime, false, false);

    }
}
