using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndLevelTrigger : MonoBehaviour
{
    public GameObject finishPanel; // Panel ucapan selamat
    //public FadeManager fadeManager;
    public LayerFader layerfade;

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
        Debug.Log("Fade Out pertama");
        yield return layerfade.StartCoroutine(layerfade.FadeOut());

        Debug.Log("Panel muncul");
        if (finishPanel != null)
            finishPanel.SetActive(true);

        Debug.Log("Fade In panel");
        yield return layerfade.StartCoroutine(layerfade.FadeIn());

        Debug.Log("Tunggu 1 detik");
        yield return new WaitForSecondsRealtime(1f);
        
        Debug.Log("Fade Out terakhir");
        yield return layerfade.StartCoroutine(layerfade.FadeOut());

        Debug.Log("Panel muncul");
        if (finishPanel != null)
            finishPanel.SetActive(false);

        Debug.Log("Scene akan diganti ke Menu...");
        SceneManager.LoadScene("Menu");
        //Debug.Log("Sudah perintah ganti scene.");*/
    }
}
