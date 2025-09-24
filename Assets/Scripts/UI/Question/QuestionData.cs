using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionData", menuName = "Quiz/Question Data")]
public class QuestionData : ScriptableObject
{
    public string questionText;
    public string[] answers = new string[4];
    public int correctAnswerIndex;
}