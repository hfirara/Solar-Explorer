using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneSoal : MonoBehaviour
{
    [SerializeField] private QuestionManager questionManager;
    [SerializeField] private PlayerUI playerUI;
    [SerializeField] private QuestionData questionData; // soal yg ingin ditampilkan

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            if (playerUI != null && playerUI.IsGamePaused()) return;

            triggered = true;
            questionManager.ShowQuestion(questionData, questionManager.AnswerQuestion);
            gameObject.SetActive(false);
        }
    }
}