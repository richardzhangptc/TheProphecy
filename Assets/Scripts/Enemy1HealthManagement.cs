using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1HealthManagement : MonoBehaviour
{
    private int maxHealth = 1;
    private int currentHealth = 1;

    public void ReduceHealth(int reduction)
    {
        currentHealth -= reduction;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void AddHealth(int addition)
    {
        currentHealth += addition;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    
}
