using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private string sceneToLoad;

    [Header("Requirement")]
    [SerializeField] private string requiredCategory = "Venus";
    [SerializeField] private int requiredInfoCount = 10;
    [SerializeField] private QuestionSequenceManager sequenceManager;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeAndLoadScene()
    {
        int collected = InventoryInfoManager.Instance.GetInfoCountByCategory(requiredCategory);

        if (collected >= requiredInfoCount && sequenceManager != null && sequenceManager.IsRunning)
        {
            StartCoroutine(FadeOutAndLoad());
        }
        else
        {
            Debug.Log($"[SceneFader] Belum memenuhi syarat. Info: {collected}/{requiredInfoCount}, Lulus Quiz: {sequenceManager?.IsRunning}");
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