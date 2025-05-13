using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class SubQuest
{
    public string description;
    public bool isCompleted;

    [System.NonSerialized] public TMP_Text uiText;
}