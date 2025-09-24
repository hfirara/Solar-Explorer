using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionManager : MonoBehaviour
{
    [SerializeField] private RocketController rocket;

    [Header("UI References")]
    public GameObject panelQuestion;
    public TMP_Text textQuestion;
    public Button[] answerButtons;
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

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i;
            answerButtons[i].GetComponentInChildren<TMP_Text>().text = question.answers[i];
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => StartCoroutine(AnswerWithDelay(index)));
            answerButtons[i].interactable = true;
            answerButtons[i].image.color = Color.white;
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
            {
                answerButtons[i].image.color = correctColor;
            }
            else if (i == selectedIndex)
            {
                answerButtons[i].image.color = wrongColor;
            }
        }

        // 🔊 Play audio di luar loop, agar tidak double
        if (isCorrect)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.correctAnswerClip);
        }
        else
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.wrongAnswerClip);
        }

        yield return new WaitForSecondsRealtime(1.5f);

        // Reset tombol
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].image.color = Color.white;
            answerButtons[i].interactable = true;
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
