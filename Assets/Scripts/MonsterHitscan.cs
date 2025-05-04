using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHitscan : MonoBehaviour
{
    private int damage = 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyHitBox")
        {
            Enemy1HealthManagement enemyHealth = other.transform.parent.GetComponent<Enemy1HealthManagement>();
            KnockbackHandler enemyKnockback = other.transform.parent.GetComponent<KnockbackHandler>();
            enemyHealth.ReduceHealth(damage);

            if (other.gameObject == null)
            {
                return;
            }

            Vector2 dir = (other.transform.position - transform.position).normalized;
            enemyKnockback.ApplyKnockbackToSelf(dir, 10, 1000f);
            
            
        }
    }
}
