using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LayerFader : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadeCanvasGroup; // objek hitam yang di-fade
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private GameObject nextPanel; // panel yang akan dimunculkan setelah fade

    public void StartFadeToPanel()
    {
        StartCoroutine(FadeOutThenShowPanel());
    }

    private IEnumerator FadeOutThenShowPanel()
    {
        // Fade out
        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Clamp01(time / fadeDuration);
            yield return null;
        }

        // Setelah layar hitam penuh, munculkan panel
        if (nextPanel != null)
            nextPanel.SetActive(true);

        // (Opsional) Fade in kembali
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvasGroup.alpha = 1f - Mathf.Clamp01(time / fadeDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = 0f;
    }
}