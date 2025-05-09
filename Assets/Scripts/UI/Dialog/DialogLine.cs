using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class DialogLine
{
    public string speakerName;
    [TextArea(2, 5)]
    public string line;
}