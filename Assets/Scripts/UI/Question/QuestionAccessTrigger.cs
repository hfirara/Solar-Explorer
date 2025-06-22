using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionAccessTrigger : MonoBehaviour
{
    public GameObject interactionKeyUI;
    public QuestionSequenceManager sequenceManager;
    public string requiredCategory = "Venus"; // Ganti sesuai kebutuhan
    public int requiredCount = 10;

    private bool isPlayerInRange = false;
    private bool hasStarted = false;

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && !hasStarted)
        {
            int collected = InventoryInfoManager.Instance.GetInfoCountByCategory(requiredCategory);

            if (collected >= requiredCount)
            {
                hasStarted = true;
                sequenceManager.gameObject.SetActive(true);
                sequenceManager.StartQuestionSequence(); // <- metode ini akan kamu buat di bawah
                Time.timeScale = 0f; // opsional
            }
            else
            {
                Debug.Log($"Info {requiredCategory} baru {collected}/{requiredCount}");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            int collected = InventoryInfoManager.Instance.GetInfoCountByCategory(requiredCategory);

            if (collected >= requiredCount)
            {
                interactionKeyUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            interactionKeyUI.SetActive(false);
        }
    }
}
