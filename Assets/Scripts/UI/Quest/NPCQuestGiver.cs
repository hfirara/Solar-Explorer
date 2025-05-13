using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCQuestGiver : MonoBehaviour
{
    [Header("Quest Info")]
    public QuestData questToGive;

    private bool playerInRange = false;
    private bool questGiven = false;

    private void Update()
    {
        if (playerInRange && !questGiven && Input.GetKeyDown(KeyCode.E))
        {
            GiveQuest();
        }
    }

    private void GiveQuest()
    {
        if (questToGive != null)
        {
            QuestManager.Instance.AddQuest(questToGive);
            questGiven = true;
            InteractionUI.Instance.ShowText(false);
            Debug.Log("Quest diberikan: " + questToGive.questTitle);
        }
        else
        {
            Debug.LogWarning("QuestData belum di-assign ke NPC!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !questGiven)
        {
            playerInRange = true;
            InteractionUI.Instance.ShowText(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            InteractionUI.Instance.ShowText(false);
        }
    }
}
