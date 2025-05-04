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
            KnockbackHandler playerKnockback = other.transform.parent.GetComponent<KnockbackHandler>();
            
            playerHealth.ReduceHealth(damage);
            
            if (other.gameObject == null)
            {
                return;
            }
            
            Vector2 dir = (other.transform.position - transform.position).normalized;
            playerKnockback.ApplyKnockbackToSelf(dir, 10, 1000f);
            
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
