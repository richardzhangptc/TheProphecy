using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1HealthManagement : MonoBehaviour
{
    [SerializeField] private Material hurtFlashMat;
    [SerializeField] private Material defaultMat;
    [SerializeField] private int maxHealth = 2;
    [SerializeField] private int currentHealth = 2;

    private void Start()
    {
        // defaultMat = GetComponent<SpriteRenderer>().material;
    }

    public void ReduceHealth(int reduction)
    {
        currentHealth -= reduction;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            return;
        }

        StartCoroutine(HurtFlash());
    }

    public void AddHealth(int addition)
    {
        currentHealth += addition;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    private IEnumerator HurtFlash()
    {
        Debug.Log("HURTFLASH");
        GetComponent<SpriteRenderer>().material = hurtFlashMat;
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().material = defaultMat;
    }
    
}
