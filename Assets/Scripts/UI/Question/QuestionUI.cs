using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionUI : MonoBehaviour
{
    public TMP_Text questionText;
    public Button[] answerButtons;
    public Color correctColor = Color.green;
    public Color wrongColor = Color.red;

    private int correctIndex;

    public void DisplayQuestion(QuestionData data)
    {
        questionText.text = data.questionText;
        correctIndex = data.correctAnswerIndex;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i;
            answerButtons[i].GetComponentInChildren<TMP_Text>().text = data.answers[i];
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => CheckAnswer(index));
            answerButtons[i].image.color = Color.white;
        }
    }

    private void CheckAnswer(int selectedIndex)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].interactable = false;

            if (i == correctIndex)
                answerButtons[i].image.color = correctColor;
            else if (i == selectedIndex)
                answerButtons[i].image.color = wrongColor;
        }
    }

    public void ResetButtons()
    {
        foreach (var btn in answerButtons)
        {
            btn.interactable = true;
            btn.image.color = Color.white;
        }
    }
}
