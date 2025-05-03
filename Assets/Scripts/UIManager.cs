using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image HurtOverlay;
    [SerializeField] private Slider healthSlider;
    #region Singleton and Awake

    public static UIManager Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    #endregion

    public void ShowHurtOverlay()
    {
        StartCoroutine(HurtOverlayRoutine());
    }

    private IEnumerator HurtOverlayRoutine()
    {
        HurtOverlay.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        HurtOverlay.gameObject.SetActive(false);
    }

    public void UpdateHealthSlider(int currentHealth)
    {
        healthSlider.value = currentHealth;
    }
}
