using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonologTrigger : MonoBehaviour
{
    public DialogData monologData;
    public QuestData subQuestToAdd;

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
        DialogManager.Instance.StartDialog(monologData);

        // Tunggu sampai dialog selesai
        while (DialogManager.Instance.IsDialogRunning)
        {
            yield return null;
        }

        // Tambahkan quest setelah dialog selesai
        if (subQuestToAdd != null)
        {
            QuestManager.Instance.AddQuest(subQuestToAdd);
        }

        gameObject.SetActive(false); // opsional
    }
}
