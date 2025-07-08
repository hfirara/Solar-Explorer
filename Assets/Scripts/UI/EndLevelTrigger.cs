using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndLevelTrigger : MonoBehaviour
{
    public GameObject finishPanel; // Panel ucapan selamat
    public FadeManager fadeManager;
    public string menuSceneName = "MenuScene"; // Ganti dengan nama scene menu kamu

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasTriggered) return;
        if (collision.CompareTag("Player"))
        {
            hasTriggered = true;
            StartCoroutine(FinishSequence());
        }
    }

    private IEnumerator FinishSequence()
    {
        // Step 1: Fade out
        yield return fadeManager.StartCoroutine(fadeManager.FadeOut());

        // Step 2: Tampilkan panel ucapan
        if (finishPanel != null)
            finishPanel.SetActive(true);

        // Step 3: Fade in (biar panel kelihatan)
        yield return fadeManager.StartCoroutine(fadeManager.FadeIn());

        // Step 4: Tunggu player klik atau delay
        yield return new WaitForSeconds(3f); // Misalnya 3 detik

        // Step 5: Fade out lagi
        yield return fadeManager.StartCoroutine(fadeManager.FadeOut());

        // Step 6: Ganti scene
        SceneManager.LoadScene(menuSceneName);
    }
}
