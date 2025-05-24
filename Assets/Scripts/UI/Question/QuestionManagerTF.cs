using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionManagerTF : MonoBehaviour
{
    [SerializeField] private RocketController rocket;

    [Header("UI References")]
    public GameObject panelQuestion;
    public TMP_Text textQuestion;
    public Button[] answerButtons; // [0] = True, [1] = False
    public Color correctColor = Color.green;
    public Color wrongColor = Color.red;

    private QuestionData currentQuestion;
    private System.Action<bool> onAnswerResult;
    private int correctIndex;

    public void ShowQuestion(QuestionData question, System.Action<bool> callback)
    {
        currentQuestion = question;
        onAnswerResult = callback;

        panelQuestion.SetActive(true);
        Time.timeScale = 0f;

        textQuestion.text = question.questionText;
        correctIndex = question.correctAnswerIndex;

        // True button = index 0, False button = index 1
        string trueText = question.answers[0];
        string falseText = question.answers[1];

        answerButtons[0].GetComponentInChildren<TMP_Text>().text = trueText;
        answerButtons[1].GetComponentInChildren<TMP_Text>().text = falseText;

        answerButtons[0].onClick.RemoveAllListeners();
        answerButtons[1].onClick.RemoveAllListeners();

        answerButtons[0].onClick.AddListener(() => StartCoroutine(AnswerWithDelay(0)));
        answerButtons[1].onClick.AddListener(() => StartCoroutine(AnswerWithDelay(1)));

        foreach (var btn in answerButtons)
        {
            btn.interactable = true;
            btn.image.color = Color.white;
        }
    }

    private IEnumerator AnswerWithDelay(int selectedIndex)
    {
        bool isCorrect = selectedIndex == correctIndex;

        // Warna tombol
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].interactable = false;

            if (i == correctIndex)
                answerButtons[i].image.color = correctColor;
            else if (i == selectedIndex)
                answerButtons[i].image.color = wrongColor;
        }

        yield return new WaitForSecondsRealtime(1.5f);

        foreach (var btn in answerButtons)
        {
            btn.image.color = Color.white;
            btn.interactable = true;
        }

        panelQuestion.SetActive(false);
        Time.timeScale = 1f;

        onAnswerResult?.Invoke(isCorrect);
    }

    public void AnswerQuestion(bool isCorrect)
    {
        if (isCorrect)
        {
            rocket.Dodge(Vector2.up, 4f, 0.4f); // kalau mau lebih eksplisit
        }
        else
        {
            rocket.TakeDamage();
        }
    }
}
