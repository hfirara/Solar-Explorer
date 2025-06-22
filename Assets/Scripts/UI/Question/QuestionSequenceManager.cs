using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionSequenceManager : MonoBehaviour
{
    public static QuestionSequenceManager Instance { get; private set; }

    [Header("Managers")]
    public QuestionManager multipleChoiceManager;
    public QuestionManagerTF trueFalseManager;

    [Header("Fade Settings")]
    public CanvasGroup fadeCanvas;
    public float fadeDuration = 1f;

    [Header("Question Data")]
    public List<QuestionData> allQuestions;

    [Header("Result Panel")]
    public GameObject panelResult;
    public TMP_Text textScore;
    public Image[] stars;

    private int currentQuestionIndex = 0;
    private int correctCount = 0;
    private bool isRunning = false;

    public bool IsRunning => isRunning;
    public int GetCorrectCount() => correctCount;

    private void Awake()
    {
        panelResult.SetActive(false);
        fadeCanvas.alpha = 1f;
        gameObject.SetActive(false); // Agar tidak langsung aktif
    }

    public void StartQuestionSequence()
    {
        // Reset jika diakses ulang
        panelResult.SetActive(false);
        currentQuestionIndex = 0;
        correctCount = 0;
        isRunning = true;

        StartCoroutine(SequenceRoutine());
    }

    private IEnumerator SequenceRoutine()
    {
        yield return FadeIn();
        yield return ShowNextQuestion();
    }

    private IEnumerator ShowNextQuestion()
    {
        if (currentQuestionIndex >= allQuestions.Count)
        {
            yield return FadeOut();
            ShowResult();
            isRunning = false;
            yield return FadeIn();
            yield break;
        }

        var question = allQuestions[currentQuestionIndex];
        bool isAnswered = false;
        bool isCorrect = false;

        System.Action<bool> callback = (result) => {
            isAnswered = true;
            isCorrect = result;
        };

        // Tentukan jenis soal
        if (question.answers.Length == 2)
            trueFalseManager.ShowQuestion(question, callback);
        else
            multipleChoiceManager.ShowQuestion(question, callback);

        yield return new WaitUntil(() => isAnswered);

        if (isCorrect) correctCount++;

        currentQuestionIndex++;

        yield return FadeOut();
        yield return FadeIn();
        yield return ShowNextQuestion();
    }

    private void ShowResult()
    {
        panelResult.SetActive(true);
        textScore.text = $"Benar: {correctCount} dari {allQuestions.Count}";

        int starCount = Mathf.RoundToInt((correctCount / (float)allQuestions.Count) * 3);
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].enabled = i < starCount;
        }
    }

    private IEnumerator FadeOut()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            fadeCanvas.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }
    }

    private IEnumerator FadeIn()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            fadeCanvas.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            yield return null;
        }
    }
}
