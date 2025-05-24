using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneSoalTF : MonoBehaviour
{
    [SerializeField] private QuestionManagerTF questionManagerTF;
    [SerializeField] private PlayerUI playerUI;
    [SerializeField] private QuestionData questionData;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            if (playerUI != null && playerUI.IsGamePaused()) return;

            triggered = true;
            questionManagerTF.ShowQuestion(questionData, questionManagerTF.AnswerQuestion);
            gameObject.SetActive(false);
        }
    }
}
