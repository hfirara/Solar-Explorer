using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UINotification : MonoBehaviour
{
    public static UINotification Instance;

    [SerializeField] private TMP_Text notificationText;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float displayTime = 2f;
    [SerializeField] private float fadeSpeed = 2f;

    private Coroutine currentCoroutine;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // Pastikan canvas group awal transparan
        canvasGroup.alpha = 0f;
    }

    public void ShowNotification(string message)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(FadeNotification(message));
    }

    private IEnumerator FadeNotification(string message)
    {
        notificationText.text = message;

        // Fade in
        while (canvasGroup.alpha < 1f)
        {
            canvasGroup.alpha += Time.unscaledDeltaTime * fadeSpeed;
            yield return null;
        }

        // Tahan
        yield return new WaitForSecondsRealtime(displayTime);

        // Fade out
        while (canvasGroup.alpha > 0f)
        {
            canvasGroup.alpha -= Time.unscaledDeltaTime * fadeSpeed;
            yield return null;
        }

        currentCoroutine = null;
    }
}
