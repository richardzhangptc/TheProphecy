using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPull : MonoBehaviour
{
	[SerializeField] private float grabDistance;
	[SerializeField] private string grabButton;

	private bool isGrabbing; 
	private GameObject currentObject; 

	private float MoveForce = 0.0f;
	private Vector2 MoveDirection = Vector2.zero;
	private Vector2 GrabDirection = Vector2.zero;

	void Start()
	{
		isGrabbing = false;
	}

	void Update()
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, GrabDirection, grabDistance, LayerMask.GetMask("Objects"));
		Debug.DrawRay(transform.position, GrabDirection * grabDistance, hit.collider ? Color.red : Color.green);

		if (!isGrabbing && Input.GetKeyDown(grabButton) && hit != null && hit.transform != null)
		{
			GameObject go = hit.transform.gameObject;
			if (go.tag == "pullable")
			{
				isGrabbing = true;
				go.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
				currentObject = go;
			}
		}
		else if (isGrabbing && (Input.GetKeyDown(grabButton) || Vector2.Distance(transform.position, currentObject.transform.position) > (grabDistance * 1.2)))
		{
			isGrabbing = false;
			currentObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
			currentObject = null;
		} 
	}

	// A script attacted to the player
	public void UpdateForces(float moveForce, Vector2 moveDirection)
	{
		MoveForce = moveForce;
		MoveDirection = moveDirection;
		if (moveDirection != Vector2.zero)
		{
			GrabDirection = moveDirection;
		}
	}

	void FixedUpdate()
	{
		if (isGrabbing)
		{
			currentObject.GetComponent<Rigidbody2D>().AddForce(MoveForce * MoveDirection);
		}
	}

}
