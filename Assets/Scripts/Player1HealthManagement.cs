using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player1HealthManagement : MonoBehaviour
{
    private int maxHealth = 10;
    private int currentHealth = 10;
    [SerializeField] private Material hurtFlashMat;
    [SerializeField] private Material defaultMat;

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
        StartCoroutine(HurtFlash());
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
    
    private IEnumerator HurtFlash()
    {
        for (int i = 0; i < 3; i++)
        {
            GetComponent<SpriteRenderer>().material = hurtFlashMat;
            yield return new WaitForSeconds(0.1f);
            GetComponent<SpriteRenderer>().material = defaultMat;
            yield return new WaitForSeconds(0.1f);
            
        }
    }
    
    
}
