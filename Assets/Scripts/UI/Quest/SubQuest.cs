using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SubQuest
{
    public string description;
    public bool isCompleted;

    [System.NonSerialized] public Text uiText; // untuk update UI nanti
}