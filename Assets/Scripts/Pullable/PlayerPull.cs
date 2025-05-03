using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPull : MonoBehaviour
{
	[SerializeField] private Vector3 direction;
	[SerializeField] private float grabDistance;
	[SerializeField] private string grabButton;

	private bool isGrabbing; 
	private GameObject currentObject; 

	private float MoveForce = 0.0f;
	private Vector2 MoveDirection = Vector2.zero;

	void Start()
	{
		isGrabbing = false;
	}

	void Update()
	{
		RaycastHit hit; 
		if (!isGrabbing && Input.GetButton(grabButton)
		&& Physics.Raycast(transform.position, direction, out hit, grabDistance))
		{
			GameObject go = hit.transform.gameObject;
			if (go.tag == "pullable")
			{
				isGrabbing = true;
				currentObject = go;
			}
		}
		else if (isGrabbing)
		{
			isGrabbing = false;
			currentObject = null;
		}

	}

	public void UpdateForces(float moveForce, Vector2 moveDirection)
	{
		MoveForce = moveForce;
		MoveDirection = moveDirection;
	}

	void FixedUpdate()
	{
		if (isGrabbing)
		{
			currentObject.GetComponent<Rigidbody2D>().AddForce(MoveForce * MoveDirection);
		}
	}
}
