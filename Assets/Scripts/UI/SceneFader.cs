using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private string sceneToLoad;

    [Header("Quiz Reference")]
    [SerializeField] private QuestionSequenceManager sequenceManager;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeAndLoadScene()
    {
        // Cek score minimal 7
        if (sequenceManager != null && sequenceManager.CorrectCount >= 7)
        {
            StartCoroutine(FadeOutAndLoad());
        }
        else
        {
            UINotification.Instance.ShowNotification("Score kamu belum lebih dari 7");
            Debug.Log("[SceneFader] Belum memenuhi syarat: score belum lebih dari 7");
        }
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

        yield return new WaitForSeconds(0.1f);
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

        fadeCanvasGroup.gameObject.SetActive(false);
    }
}