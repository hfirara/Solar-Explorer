using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCQuestGiver : MonoBehaviour
{
    public QuestData questToGive;
    private bool playerInRange = false;
    private bool questGiven = false;

    void Update()
    {
        if (playerInRange && !questGiven && Input.GetKeyDown(KeyCode.E))
        {
            QuestManager.Instance.AddQuest(questToGive);
            questGiven = true;
            Debug.Log("Quest diberikan: " + questToGive.questTitle);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
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
