using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Info Item", menuName = "Info/Info Item")]
public class InfoItem : ScriptableObject
{
    public string infoTitle;
    [TextArea] public string infoDescription;

    public string categoryID; // Contoh: "planet_bumi"
}