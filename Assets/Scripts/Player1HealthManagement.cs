using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player1HealthManagement : MonoBehaviour
{
    private int maxHealth = 6;
    private int currentHealth = 6;

    private void Start()
    {
        UIManager.Instance.UpdateHealthSlider(currentHealth);
    }

    public void ReduceHealth(int reduction)
    {
        UIManager.Instance.ShowHurtOverlay();
        Debug.Log("HURT");
        currentHealth -= reduction;
        if (currentHealth <= 0)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
        UIManager.Instance.UpdateHealthSlider(currentHealth);
    }

    public void AddHealth(int addition)
    {
        currentHealth += addition;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UIManager.Instance.UpdateHealthSlider(currentHealth);
    }
    
    
}
