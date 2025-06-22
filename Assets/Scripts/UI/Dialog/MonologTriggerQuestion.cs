using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonologTriggerQuestion : MonoBehaviour
{
    public DialogData monologData;
    public GameObject questionPanel;
    public QuestionSequenceManager questionManager;
    public QuestData subQuestToAdd;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        triggered = true;
        StartCoroutine(HandleMonologAndQuestion());
    }

    private IEnumerator HandleMonologAndQuestion()
    {
        // Mulai monolog
        DialogManager.Instance.StartDialog(monologData);

        // Tunggu hingga selesai
        while (DialogManager.Instance.IsDialogRunning)
            yield return null;

        // Tambahkan quest (opsional)
        if (subQuestToAdd != null)
            QuestManager.Instance.AddQuest(subQuestToAdd);

        // Tampilkan panel soal dan mulai
        questionPanel.SetActive(true);
        yield return new WaitForSeconds(0.2f); // Delay kecil biar smooth
        questionManager.StartQuestionSequence();

        // Tunggu soal selesai (panel tertutup otomatis)
        yield return new WaitUntil(() => !questionPanel.activeSelf);

        gameObject.SetActive(false); // Trigger dimatikan
    }
}
