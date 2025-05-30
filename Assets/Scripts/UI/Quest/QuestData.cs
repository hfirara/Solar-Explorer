using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest System/Quest")]
public class QuestData : ScriptableObject
{
    public string questTitle;
    [TextArea] public string questDescription;

    public string targetInfoCategoryID; // contoh: "planet_bumi"
    public int targetAmount;        // contoh: 10 info
}
