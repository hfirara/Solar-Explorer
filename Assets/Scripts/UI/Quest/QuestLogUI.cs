using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestLogUI : MonoBehaviour
{
    [SerializeField] private Transform questListParent;
    [SerializeField] private GameObject questItemPrefab;

    private Dictionary<Quest, GameObject> questToUIMap = new Dictionary<Quest, GameObject>();

    public void AddQuest(Quest quest)
    {
        GameObject questGO = Instantiate(questItemPrefab, questListParent);
        TMP_Text text = questGO.GetComponentInChildren<TMP_Text>();
        text.text = quest.questTitle;
        questToUIMap[quest] = questGO;
    }

    public void UpdateQuestUI(Quest quest)
    {
        if (questToUIMap.TryGetValue(quest, out GameObject questGO))
        {
            TMP_Text text = questGO.GetComponentInChildren<TMP_Text>();
            text.color = quest.isCompleted ? Color.green : Color.white;
        }
    }

    public void RemoveQuest(Quest quest)
    {
        if (questToUIMap.TryGetValue(quest, out GameObject questGO))
        {
            Destroy(questGO);
            questToUIMap.Remove(quest);
        }
    }
}
