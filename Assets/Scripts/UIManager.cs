using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image HurtOverlay;
    [SerializeField] private Image lightOverlay;
    [SerializeField] private Slider healthSlider;
    private bool lighting = false;
        
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

    public void FadeInLightOverlay()
    {
        StartCoroutine(FadeInLightRoutine());
    }

    public void CancelLightFadeIn()
    {
        if (lighting)
        {
            StopAllCoroutines();
            lightOverlay.gameObject.SetActive(false);
            HurtOverlay.gameObject.SetActive(false);
        }
    }

    private IEnumerator FadeInLightRoutine()
    {
        lighting = true;
        lightOverlay.gameObject.SetActive(true);
        CanvasGroup lightOverlayCanvasGroup = lightOverlay.gameObject.GetComponent<CanvasGroup>();
        lightOverlayCanvasGroup.alpha = 0;
        while (lightOverlayCanvasGroup.alpha < 0.95f)
        {
            lightOverlayCanvasGroup.alpha += 0.005f;
            yield return null;
        }
        
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        lighting = false;

    }
    
    
    
}
