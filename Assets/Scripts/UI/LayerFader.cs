using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LayerFader : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private string sceneToLoad;

    private void Awake()
    {
        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 1f; // Awalnya full hitam
            fadeCanvasGroup.gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvasGroup.alpha = 1f - Mathf.Clamp01(time / fadeDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = 0f;
        fadeCanvasGroup.gameObject.SetActive(false);
    }

    public void FadeAndLoadScene()
    {
        StartCoroutine(FadeOutAndLoad());
    }

    public IEnumerator FadeOut()
    {
        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.gameObject.SetActive(true);
        }
        
        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Clamp01(time / fadeDuration);
            yield return null;
        }

        /*if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.gameObject.SetActive(true);
        }

        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Clamp01(time / fadeDuration);
            yield return null;
        }*/
    }

    public IEnumerator FadeOutAndLoad()
    {
        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.gameObject.SetActive(true);
        }

        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Clamp01(time / fadeDuration);
            yield return null;
        }

        SceneManager.LoadScene(sceneToLoad);
    }
}
