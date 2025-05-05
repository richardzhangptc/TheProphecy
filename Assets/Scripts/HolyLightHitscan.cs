using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyLightHitscan : MonoBehaviour
{
    private int damage = 1;
    private float delay = 0.15f;
    private bool blocked = false;

    public void Deactivate()
    {
        Destroy(gameObject);
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "MonsterHitBox" && blocked == false)
        {
            Player1HealthManagement playerHealth = other.transform.parent.GetComponent<Player1HealthManagement>();
            if (playerHealth == null)
            {
                return;
            }
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
