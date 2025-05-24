using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndLevelTrigger : MonoBehaviour
{
    [SerializeField] private RocketController rocket;
    [SerializeField] private GameObject fadeOutPanel;
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private TMP_Text tamatText;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private string mainMenuSceneName = "Menu";

    private bool hasEnded = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasEnded && other.CompareTag("Player"))
        {
            hasEnded = true;
            StartCoroutine(EndSequence());
        }
    }

    private IEnumerator EndSequence()
    {
        // Stop waktu
        Time.timeScale = 0f;

        // Meluncur cepat ke depan
        rocket.LaunchForward(5f);

        // Tunggu 1.5 detik secara realtime
        yield return new WaitForSecondsRealtime(1.5f);

        // Fade out ke hitam
        fadeOutPanel.SetActive(true);
        if (fadeCanvasGroup != null)
        {
            for (float t = 0; t < 1f; t += Time.unscaledDeltaTime / fadeDuration)
            {
                fadeCanvasGroup.alpha = t;
                yield return null;
            }
            fadeCanvasGroup.alpha = 1f;
        }

        // Tampilkan tulisan "Tamat"
        tamatText.gameObject.SetActive(true);
        tamatText.text = "Selamat! Kamu telah menyelesaikan misi ekspedisi planet.\nKamu astronot yang hebat 🚀";

        yield return new WaitForSecondsRealtime(3f);

        tamatText.text = "FINISH!";
        yield return new WaitForSecondsRealtime(2f);

        // Pindah ke Main Menu
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
