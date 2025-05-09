using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog/Dialog Data")]
public class DialogData : ScriptableObject
{
    public List<DialogLine> dialogLines = new List<DialogLine>();
}