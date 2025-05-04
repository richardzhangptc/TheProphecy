using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    private bool receivingMovementInput = false;
    private Vector2 moveDirection = Vector2.zero;
    private Rigidbody2D myRB;
    private Animator myAnim;
    private float movementForce = 1000f;

    private void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        GetMovementInput();
        GetAttackInput();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }
    
    private void GetMovementInput()
    {
        Vector2 moveInput = Vector2.zero;
        
        if (Input.GetKey(KeyCode.W))
            moveInput.y += 1;
        if (Input.GetKey(KeyCode.S))
            moveInput.y -= 1;
        if (Input.GetKey(KeyCode.D))
            moveInput.x += 1;
        if (Input.GetKey(KeyCode.A))
            moveInput.x -= 1;
        
        if(moveInput != Vector2.zero) //the movement input vector is receiving input
        {
            receivingMovementInput = true;
            moveDirection = moveInput.normalized;
        }
        else
        {
            receivingMovementInput = false;
            moveDirection = Vector2.zero;
        }

		PlayerPull playerPull = transform.GetComponent<PlayerPull>(); 
		if (playerPull != null) 
		{
			playerPull.UpdateForces(movementForce, moveDirection);
		}
        
    }

    private void GetAttackInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(myAnim.GetFloat("xDir"));
            Debug.Log(myAnim.GetFloat("yDir"));
            myAnim.SetTrigger("Attack");
        }
    }


    private void ApplyMovement()
    {
        if(receivingMovementInput == true)
        {
            myAnim.SetFloat("xDir", moveDirection.x);
            myAnim.SetFloat("yDir", moveDirection.y);
            myAnim.SetBool("isWalking", true);
            myRB.AddForce(moveDirection.normalized * movementForce, ForceMode2D.Force);
        }
        else
        {
            myAnim.SetBool("isWalking", false);
        }
    }
}
