using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Info Item", menuName = "Info/Info Item")]
public class InfoItem : ScriptableObject
{
    public List<DataLine> dataLines = new List<DataLine>();
}