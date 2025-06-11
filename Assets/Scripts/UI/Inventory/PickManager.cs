using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PickManager : MonoBehaviour
{
    public static PickManager Instance;

    [Header("UI References")]
    [SerializeField] private GameObject dataPanel;
    [SerializeField] private TMP_Text dataText;
    [SerializeField] private Button dataButton;
    [SerializeField] private InteractionUI interactionKeyUI;

    [Header("Detection")]
    [SerializeField] private float detectionRadius = 2f;
    [SerializeField] private LayerMask pickLayer;
    [SerializeField] private Transform playerTransform;

    private PickTrigger currentTrigger;
    private readonly List<PickTrigger> triggersInRange = new List<PickTrigger>();
    private List<DataLine> currentData;
    private int currentIndex = 0;

    private bool isDataRunning = false;
    public bool IsDataRunning => isDataRunning;
    public bool IsDataActive => dataPanel.activeSelf;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        dataPanel.SetActive(false);
        if (dataButton != null)
            dataButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!isDataRunning)
        {
            FindNearestTrigger();

            /*if (currentTrigger != null && Input.GetKeyDown(KeyCode.E))
            {
                StartData(currentTrigger.infoItem, currentTrigger);
            }*/
        }
    }

    public void RegisterTrigger(PickTrigger trigger)
    {
        if (!triggersInRange.Contains(trigger))
            triggersInRange.Add(trigger);
    }

    public void UnregisterTrigger(PickTrigger trigger)
    {
        if (triggersInRange.Contains(trigger))
            triggersInRange.Remove(trigger);

        if (trigger == currentTrigger)
        {
            currentTrigger = null;
            ShowInteractKey(false);
        }
    }


    private void FindNearestTrigger()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(playerTransform.position, detectionRadius, pickLayer);

        float closestDistance = float.MaxValue;
        PickTrigger closestTrigger = null;

        foreach (var hit in hits)
        {
            PickTrigger trigger = hit.GetComponent<PickTrigger>();
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
                ShowInteractKey(true, currentTrigger.transform.position);
        }
    }

    public void StartData(InfoItem data, PickTrigger trigger)
    {
        if (data == null || data.dataLines == null || data.dataLines.Count == 0) return;
        if (isDataRunning) return;

        UIManagerData.Instance.SetDataActive(true);

        currentData = data.dataLines;
        currentIndex = 0;
        currentTrigger = trigger;
        isDataRunning = true;

        dataPanel.SetActive(true);
        dataText.text = currentData[currentIndex].description;

        if (dataButton != null)
            dataButton.gameObject.SetActive(true);

        ShowInteractKey(false);
    }

    public void ShowInteractKey(bool show, Vector3? position = null)
    {
        if (interactionKeyUI == null) return;

        if (position.HasValue)
            interactionKeyUI.Show("E", position.Value);
        else
            interactionKeyUI.Hide();
    }

    public void CloseData()
    {
        dataPanel.SetActive(false);
        if (dataButton != null)
            dataButton.gameObject.SetActive(false);

        isDataRunning = false;
        UIManagerData.Instance.SetDataActive(false);

        if (currentTrigger != null)
        {
            QuestManager.Instance.OnInfoCollected(currentTrigger.infoItem.categoryID);
            InventoryInfoManager.Instance.AddInfo(currentTrigger.infoItem);

            Destroy(currentTrigger.gameObject);
            currentTrigger = null;
        }

        ShowInteractKey(false);
    }
}