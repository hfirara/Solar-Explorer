using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public string questTitle;
    public string questDescription;
    public bool isCompleted;
    public bool isTemporarySubQuest;

    public string targetInfoCategoryID; // misal "planet_bumi"
    public string targetNpcID;
    public int targetAmount;
    public int currentAmount;
}
