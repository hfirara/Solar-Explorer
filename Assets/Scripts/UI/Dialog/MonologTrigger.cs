using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonologTrigger : MonoBehaviour
{
    public DialogData monologData;
    public QuestData QuestToAdd;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(HandleMonologThenQuest());
        }
    }

    private IEnumerator HandleMonologThenQuest()
    {
        // Mulai dialog
        DialogManager.Instance.StartDialog(monologData);

        // Tunggu sampai dialog selesai
        while (DialogManager.Instance.IsDialogRunning)
        {
            yield return null;
        }

        // Tambahkan quest
        if (QuestToAdd != null)
        {
            QuestManager.Instance.AddQuest(QuestToAdd);

            // Munculkan notif
            UINotification.Instance.ShowNotification("Quest baru ditambahkan!");
        }

        // Opsional: Nonaktifkan trigger
        gameObject.SetActive(false);
    }
}
