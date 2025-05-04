using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class VillagerController : MonoBehaviour
{
    private AIPathNew pathScript;
    private float movementForce = 500f;
    private Rigidbody2D myRB;
    public Vector3 waypointDir;

    private void Start()
    {
        pathScript = GetComponent<AIPathNew>();
        myRB = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        waypointDir = pathScript.dirToWaypoint;
        if (waypointDir != null)
        {
            ApplyMovement();
        }
    }

    
    private void ApplyMovement()
    {
        myRB.AddForce(waypointDir.normalized * movementForce, ForceMode2D.Force);
    }
}
