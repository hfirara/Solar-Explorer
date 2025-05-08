using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    public List<Quest> quests = new List<Quest>();
    public QuestLogUI questLogUI;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (var quest in quests)
        {
            questLogUI.AddQuest(quest);
        }
    }

    public void CompleteSubQuest(int questIndex, int subIndex)
    {
        quests[questIndex].subQuests[subIndex].isCompleted = true;
        questLogUI.UpdateQuestUI(quests[questIndex]);
    }

    public void AddQuest(QuestData questData)
    {
        Quest newQuest = new Quest
        {
            questTitle = questData.questTitle,
            questDescription = questData.questDescription,
            isCompleted = false,
            subQuests = new List<SubQuest>()
        };

        // Clone subQuests dari QuestData ke Quest runtime
        foreach (var sub in questData.subQuests)
        {
            newQuest.subQuests.Add(new SubQuest { description = sub.description, isCompleted = false });
        }

        quests.Add(newQuest);
        questLogUI.AddQuest(newQuest);
    }
}
