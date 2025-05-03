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
            enemyHealth.ReduceHealth(damage);
        }
    }
}
