using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private string sceneToLoad;

    public void FadeAndLoadScene()
    {
        StartCoroutine(FadeOutAndLoad());
    }

    private IEnumerator FadeOutAndLoad()
    {
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Clamp01(time / fadeDuration);
            yield return null;
        }

        // Ganti ke scene berikutnya setelah fade selesai
        SceneManager.LoadScene(sceneToLoad);
    }
}