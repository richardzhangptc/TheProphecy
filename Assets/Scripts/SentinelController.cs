using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class SentinelController : MonoBehaviour
{
    [SerializeField] private float startingAngle;
    public float rotationSpeed = 180f; // Degrees per second
    private float targetAngle;
    private bool shouldRotate = false;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, startingAngle);
    }

    public void RotateToTargetAngleDeg(float tAngle)
    {
        targetAngle = tAngle;
        shouldRotate = true;
    }

    private void Update()
    {
        if (shouldRotate)
        {
            float currentZ = transform.eulerAngles.z;
            float newZ = Mathf.MoveTowardsAngle(currentZ, targetAngle, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, 0f, newZ);

            if (Mathf.Abs(Mathf.DeltaAngle(newZ, targetAngle)) < 0.1f)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, targetAngle);
                shouldRotate = false;
            }
        }
    }

}