using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    public List<Quest> activeQuests = new List<Quest>();
    public QuestLogUI questLogUI;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // Tambah quest utama
        // Ini bisa juga lewat NPC nanti
    }

    public void AddQuest(QuestData data, bool isTemporary = false)
    {
        Quest newQuest = new Quest
        {
            questTitle = data.questTitle,
            questDescription = data.questDescription,
            isCompleted = false,
            isTemporarySubQuest = isTemporary,

            targetInfoCategoryID = data.targetInfoCategoryID,
            targetAmount = data.targetAmount,
            currentAmount = 0
        };

        activeQuests.Add(newQuest);
        questLogUI.AddQuest(newQuest);
    }

    public void OnInfoCollected(string categoryID)
    {
        foreach (Quest quest in activeQuests)
        {
            if (!quest.isCompleted && quest.targetInfoCategoryID == categoryID)
            {
                quest.currentAmount++;

                if (quest.currentAmount >= quest.targetAmount)
                {
                    CompleteQuest(quest.questTitle);
                }
                else
                {
                    questLogUI.UpdateQuestUI(quest);
                }
            }
        }
    }

    public void CompleteQuest(string title)
    {
        Quest quest = activeQuests.Find(q => q.questTitle == title);
        if (quest != null)
        {
            quest.isCompleted = true;
            questLogUI.UpdateQuestUI(quest);

            if (quest.isTemporarySubQuest)
            {
                RemoveQuest(quest);
            }
        }
    }

    public void RemoveQuest(Quest quest)
    {
        activeQuests.Remove(quest);
        questLogUI.RemoveQuest(quest);
    }
}
