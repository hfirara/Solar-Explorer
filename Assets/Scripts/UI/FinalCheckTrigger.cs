using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

/*public class FinalCheckTrigger : MonoBehaviour
{
    public CanvasGroup fadeCanvas;
    public float fadeDuration = 1f;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered || !other.CompareTag("Player")) return;

        // Cek info
        int infoCount = new List<InfoItem>(InventoryInfoManager.Instance.GetAllInfo()).Count;
        int correctAnswers = QuestionSequenceManager.Instance.TotalCorrectAnswer;

        if (infoCount >= 10 && correctAnswers >= 7)
        {
            triggered = true;
            StartCoroutine(FadeOut());
        }
        else
        {
            Debug.Log("Belum memenuhi syarat untuk lanjut: info < 10 atau jawaban benar < 7");
        }
    }

    private IEnumerator FadeOut()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            fadeCanvas.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }

        // TODO: Pindah scene atau tampilkan "Tamat"
        Debug.Log("Fade selesai, lanjut ke akhir!");
    }
}*/