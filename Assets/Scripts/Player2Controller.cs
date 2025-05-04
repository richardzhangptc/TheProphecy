using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    private bool receivingMovementInput = false;
    private Vector2 moveDirection = Vector2.zero;
    private Rigidbody2D myRB;
    private float movementForce = 1000f;
    private Animator myAnim;

    private void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        GetMovementInput();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void GetMovementInput()
    {
        Vector2 moveInput = Vector2.zero;
        
        if (Input.GetKey(KeyCode.UpArrow))
            moveInput.y += 1;
        if (Input.GetKey(KeyCode.DownArrow))
            moveInput.y -= 1;
        if (Input.GetKey(KeyCode.RightArrow))
            moveInput.x += 1;
        if (Input.GetKey(KeyCode.LeftArrow))
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


    private void ApplyMovement()
    {
        if(receivingMovementInput == true)
        {
            myAnim.SetFloat("xDir", moveDirection.x);
            myAnim.SetFloat("yDir", moveDirection.y);
            myRB.AddForce(moveDirection.normalized * movementForce, ForceMode2D.Force);
        }
    }
}
