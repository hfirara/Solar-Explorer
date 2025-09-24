using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogTrigger : MonoBehaviour
{
    public DialogData dialogData;
    public string npcID;
    public QuestData QuestToAdd;

    private bool playerInRange = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            DialogManager.Instance.ShowInteractKey(true, transform.position);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            DialogManager.Instance.ShowInteractKey(false);
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (QuestManager.Instance.activeQuests.Exists(q => !q.isCompleted && q.targetNpcID == npcID))
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.pickupInfoClip);

                // Jalankan dialog + callback
                DialogManager.Instance.StartDialog(dialogData, () =>
                {
                    // Callback setelah dialog selesai
                    QuestManager.Instance.OnTalkedToNPC(npcID);

                    if (QuestToAdd != null)
                    {
                        QuestManager.Instance.AddQuest(QuestToAdd);
                        UINotification.Instance.ShowNotification("Quest baru ditambahkan!");
                    }
                });
            }
            else
            {
                Debug.Log("NPC ini bukan target quest saat ini!");
                UINotification.Instance.ShowNotification("NPC ini bukan target quest saat ini!");
            }
        }
    }
}