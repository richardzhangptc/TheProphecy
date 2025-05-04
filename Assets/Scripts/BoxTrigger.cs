using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTrigger : MonoBehaviour
{
    private Rigidbody2D myRB;

    private void Start()
    {
        myRB = GetComponentInParent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Platform")
        {
			myRB.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
