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
    private float movementForce = 500f;
    public bool frozen = false;

    [SerializeField] private GameObject attackEffect;

    private void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        GetMovementInput();
        GetAttackInput();

        if (frozen)
        {
            myAnim.SetBool("isFrozen", true);
        }
        else
        {
            myAnim.SetBool("isFrozen", false);
        }
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
            float x = myAnim.GetFloat("xDir");
            float y = myAnim.GetFloat("yDir");

            Vector2 rawDir = new Vector2(x, y);

            Vector2 attackDir;
            if (Mathf.Abs(x) > Mathf.Abs(y))
                attackDir = new Vector2(Mathf.Sign(x), 0f);
            else
                attackDir = new Vector2(0f, Mathf.Sign(y));

            float angle = 0f;
            if (attackDir == Vector2.up)
                angle = 90f;
            else if (attackDir == Vector2.left)
                angle = 180f;
            else if (attackDir == Vector2.down)
                angle = 270f;
            
            Vector3 localOffset = attackDir * 0.5f;
            GameObject effect = Instantiate(attackEffect, transform.position, Quaternion.Euler(0f, 0f, angle), transform);
            effect.transform.localPosition = localOffset;

            myAnim.SetTrigger("Attack");
        }
    }



    private void ApplyMovement()
    {
        if (frozen == true)
        {
            return;
        }
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
