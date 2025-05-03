using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Hitscan : MonoBehaviour
{
    private int damage = 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerHitBox")
        {
            Debug.Log("p1");
            Player1HealthManagement playerHealth = other.transform.parent.GetComponent<Player1HealthManagement>();
            playerHealth.ReduceHealth(damage);
        }
    }
}
