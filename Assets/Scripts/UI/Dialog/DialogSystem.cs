using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [Header("Dialog UI")]
    public GameObject dialogPanel;
    public Text dialogText;
    public Button nextButton;

    [Header("Dialog Content")]
    [TextArea(2, 5)]
    public string[] dialogLines;

    private int currentLine = 0;

    private void Start()
    {
        dialogPanel.SetActive(false);
        nextButton.onClick.AddListener(NextDialog);
    }

    public void StartDialog()
    {
        currentLine = 0;
        dialogPanel.SetActive(true);
        dialogText.text = dialogLines[currentLine];
    }

    public void NextDialog()
    {
        currentLine++;
        if (currentLine < dialogLines.Length)
        {
            dialogText.text = dialogLines[currentLine];
        }
        else
        {
            dialogPanel.SetActive(false);
        }
    }
}
