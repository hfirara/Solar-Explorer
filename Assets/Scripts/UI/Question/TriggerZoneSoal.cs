using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneSoal : MonoBehaviour
{
    [SerializeField] private GameObject soalPanel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            soalPanel.SetActive(true);
            Time.timeScale = 0f; // pause game saat soal muncul
        }
    }

    public void CloseSoalPanel()
    {
        soalPanel.SetActive(false);
        Time.timeScale = 1f; // resume game
        Destroy(gameObject); // agar soal ini tidak muncul lagi
    }
}
