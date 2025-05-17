using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionData", menuName = "Quiz/Question Data")]
public class QuestionData : ScriptableObject
{
    [TextArea]
    public string questionText;
    public List<string> answers = new List<string>();
    public int correctAnswerIndex;
}