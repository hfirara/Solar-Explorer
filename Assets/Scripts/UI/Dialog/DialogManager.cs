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
    [SerializeField] private InteractionUI interactionKeyUI;

    [Header("Detection")]
    [SerializeField] private float detectionRadius = 2f;
    [SerializeField] private LayerMask pickLayer;
    [SerializeField] private Transform playerTransform;

    private NPCDialogTrigger currentTrigger;
    private readonly List<DialogLine> triggersInRange = new List<DialogLine>();
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

    private void Update()
    {
        if (!isDialogRunning)
        {
            FindNearestTrigger();
        }
    }

    private void FindNearestTrigger()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(playerTransform.position, detectionRadius, pickLayer);

        float closestDistance = float.MaxValue;
        NPCDialogTrigger closestTrigger = null;

        foreach (var hit in hits)
        {
            NPCDialogTrigger trigger = hit.GetComponent<NPCDialogTrigger>();
            if (trigger != null)
            {
                float dist = Vector2.Distance(playerTransform.position, hit.transform.position);
                if (dist < closestDistance)
                {
                    closestDistance = dist;
                    closestTrigger = trigger;
                }
            }
        }

        if (closestTrigger != currentTrigger)
        {
            ShowInteractKey(false);
            currentTrigger = closestTrigger;

            if (currentTrigger != null)
            {
                ShowInteractKey(true, currentTrigger.transform.position);
            }
        }
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

    public void ShowInteractKey(bool show, Vector3? position = null)
    {
        if (interactionKeyUI == null) return;

        if (position.HasValue)
            interactionKeyUI.Show("E", position.Value);
        else
            interactionKeyUI.Hide();
    }
}
