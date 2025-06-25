using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionAccessTrigger : MonoBehaviour
{
    public GameObject interactionKeyUI;
    public TMP_Text infoTextUI; // << tambahkan ini
    public QuestionSequenceManager sequenceManager;
    public string requiredCategory = "Venus";
    public int requiredCount = 10;

    private bool isPlayerInRange = false;
    private bool hasStarted = false;

    private void Update()
    {
        if (isPlayerInRange)
        {
            int collected = InventoryInfoManager.Instance.GetInfoCountByCategory(requiredCategory);

            // Update UI teks setiap frame
            if (infoTextUI != null)
            {
                infoTextUI.text = $"Info dikumpulkan: {collected}/{requiredCount}";
                infoTextUI.color = (collected >= requiredCount) ? Color.green : Color.white;
            }

            if (Input.GetKeyDown(KeyCode.E) && !hasStarted)
            {
                if (collected >= requiredCount)
                {
                    hasStarted = true;
                    sequenceManager.gameObject.SetActive(true);
                    sequenceManager.StartQuestionSequence();
                    Time.timeScale = 0f;
                }
                else
                {
                    Debug.Log($"Belum cukup info: {collected}/{requiredCount}");
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;

            if (interactionKeyUI != null)
                interactionKeyUI.SetActive(true);

            if (infoTextUI != null)
                infoTextUI.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;

            if (interactionKeyUI != null)
                interactionKeyUI.SetActive(false);

            if (infoTextUI != null)
                infoTextUI.gameObject.SetActive(false);
        }
    }
}
