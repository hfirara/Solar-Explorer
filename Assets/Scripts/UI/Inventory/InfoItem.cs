using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Info Item", menuName = "Info/Info Item")]
public class InfoItem : ScriptableObject
{
    public string categoryID;
    public List<DataLine> dataLines = new List<DataLine>();
}