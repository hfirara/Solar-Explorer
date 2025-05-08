using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public string questTitle;
    public string questDescription;
    public bool isCompleted;
    
    public List<SubQuest> subQuests = new List<SubQuest>();
}