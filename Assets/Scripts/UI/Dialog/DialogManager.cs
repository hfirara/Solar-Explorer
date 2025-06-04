using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogManager : MonoBehaviour
{
   public static DialogManager Instance;

    [Header("UI References")]
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private TMP_Text speakerNameText;
    [SerializeField] private Button nextButton;
    [SerializeField] private GameObject interactionKeyUI;

    private List<DialogLine> currentDialog;
    private string currentSpeaker;
    private int currentIndex = 0;

    private bool isDialogRunning = false;
    public bool IsDialogRunning => isDialogRunning;
    public bool IsDialogActive => dialogPanel.activeSelf;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        dialogPanel.SetActive(false);
        nextButton.onClick.AddListener(NextLine);
    }

    public void StartDialog(DialogData data)
    {
        if (data == null || data.dialogLines == null || data.dialogLines.Count == 0)
            return;

        UIManager.Instance.SetDialogActive(true); // << Tambahkan ini

        currentDialog = data.dialogLines;
        currentIndex = 0;

        dialogPanel.SetActive(true);
        speakerNameText.text = currentDialog[currentIndex].speakerName;
        dialogText.text = currentDialog[currentIndex].line;
    }

    private void NextLine()
    {
        currentIndex++;
        if (currentIndex < currentDialog.Count)
        {
            speakerNameText.text = currentDialog[currentIndex].speakerName;
            dialogText.text = currentDialog[currentIndex].line;
        }
        else
        {
            EndDialog();
        }
    }

    private void EndDialog()
    {
        dialogPanel.SetActive(false);
        currentDialog = null;
        currentIndex = 0;

        UIManager.Instance.SetDialogActive(false); // << Tambahkan ini
    }

    public void ShowInteractKey(bool show)
    {
        if (interactionKeyUI != null)
            interactionKeyUI.SetActive(show);
    }
}
