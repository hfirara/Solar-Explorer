using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogTrigger : MonoBehaviour
{
    public DialogData dialogData;
    public string npcID;
    public QuestData QuestToAdd;

    private bool playerInRange = false;
    private bool dialogStarted = false; // supaya tidak double trigger

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            DialogManager.Instance.ShowInteractKey(true);
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
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !dialogStarted)
        {
            bool isCorrectNPC = QuestManager.Instance.activeQuests.Exists(q => !q.isCompleted && q.targetNpcID == npcID);

            if (isCorrectNPC)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.pickupInfoClip);

                // Hitung quest
                QuestManager.Instance.OnTalkedToNPC(npcID);

                // Mulai coroutine handle dialog + quest
                StartCoroutine(HandleDialogThenQuest());
                dialogStarted = true;
            }
            else
            {
                UINotification.Instance.ShowNotification("NPC ini bukan target quest saat ini!");
                Debug.Log("NPC ini bukan target quest saat ini!");
            }
        }
    }

    private IEnumerator HandleDialogThenQuest()
    {
        // Mulai dialog
        DialogManager.Instance.StartDialog(dialogData);

        // Tunggu sampai dialog selesai
        while (DialogManager.Instance.IsDialogRunning)
        {
            yield return null;
        }

        // Tambah quest baru jika ada
        if (QuestToAdd != null)
        {
            QuestManager.Instance.AddQuest(QuestToAdd);
            Debug.Log($"Quest baru '{QuestToAdd.questTitle}' ditambahkan setelah dialog.");
        }

        // Opsional: disable trigger supaya tidak bisa diulang
        //gameObject.SetActive(false);
    }
}
