using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneFader : MonoBehaviour
{
     [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration = 0.5f; // ← fade lebih cepat
    [SerializeField] private string sceneToLoad;

    private void Start()
    {
        // Saat scene dimulai, lakukan fade in
        StartCoroutine(FadeIn());
    }

    public void FadeAndLoadScene()
    {
        StartCoroutine(FadeOutAndLoad());
    }

    private IEnumerator FadeOutAndLoad()
    {
        fadeCanvasGroup.gameObject.SetActive(true);
        fadeCanvasGroup.alpha = 0f;

        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Clamp01(time / fadeDuration);
            yield return null;
        }

        yield return new WaitForSeconds(0.1f); // sedikit jeda
        SceneManager.LoadScene(sceneToLoad);
    }

    private IEnumerator FadeIn()
    {
        fadeCanvasGroup.gameObject.SetActive(true);
        fadeCanvasGroup.alpha = 1f;

        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvasGroup.alpha = 1f - Mathf.Clamp01(time / fadeDuration);
            yield return null;
        }

        fadeCanvasGroup.gameObject.SetActive(false); // opsional sembunyikan
    }
}