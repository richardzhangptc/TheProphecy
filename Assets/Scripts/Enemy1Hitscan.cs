using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Hitscan : MonoBehaviour
{
    private int damage = 1;
    private int delay = 1;
    private bool blocked = false;
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "PlayerHitBox" && blocked == false)
        {
            Player1HealthManagement playerHealth = other.transform.parent.GetComponent<Player1HealthManagement>();
            playerHealth.ReduceHealth(damage);
            blocked = true;
            StartCoroutine(ExpireBlock());
        }
    }

    private IEnumerator ExpireBlock()
    {
        yield return new WaitForSeconds(delay);
        blocked = false;
    }
}
