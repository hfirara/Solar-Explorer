using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Info Item", menuName = "Info/Info Item")]
public class InfoItem : ScriptableObject
{
    public string title;         // Judul info
    [TextArea]
    public string description;   // Isi informasinya

    public string categoryID;    // Contoh: "planet_bumi"
}