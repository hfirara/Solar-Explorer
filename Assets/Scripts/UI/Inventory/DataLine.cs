using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class DataLine
{
    public string infoDescription;

    public string categoryID; // Contoh: "planet_bumi"
    [TextArea] public string line;
}