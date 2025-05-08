using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestLogUI : MonoBehaviour
{
    [SerializeField] private Transform questListParent;
    [SerializeField] private GameObject questItemPrefab;
    [SerializeField] private GameObject subQuestItemPrefab;

    private Dictionary<Quest, GameObject> questToUIMap = new Dictionary<Quest, GameObject>();

    public void AddQuest(Quest quest)
    {
        GameObject questGO = Instantiate(questItemPrefab, questListParent);
        questGO.GetComponentInChildren<Text>().text = quest.questTitle;
        questToUIMap[quest] = questGO;

        foreach (var sub in quest.subQuests)
        {
            GameObject subGO = Instantiate(subQuestItemPrefab, questGO.transform);
            Text subText = subGO.GetComponentInChildren<Text>();
            subText.text = "- " + sub.description;

            sub.uiText = subText;
        }
    }

    public void UpdateQuestUI(Quest quest)
    {
        foreach (var sub in quest.subQuests)
        {
            if (sub.uiText != null)
                sub.uiText.color = sub.isCompleted ? Color.green : Color.white;
        }
    }
}
