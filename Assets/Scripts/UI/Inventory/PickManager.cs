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

    private PickTrigger currentTrigger;

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
            dataButton.gameObject.SetActive(false); // pastikan tombol juga disembunyikan di awal
    }

    public void StartData(InfoItem data, PickTrigger trigger)
    {
        if (data == null || data.dataLines == null || data.dataLines.Count == 0)
            return;

        if (isDataRunning) return; // Cegah double trigger

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

    public void ShowInteractKey(bool show)
    {
        if (interactionKeyUI == null) return;

        if (show)
            interactionKeyUI.Show("E");
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

        ShowInteractKey(false); 

        if (currentTrigger != null)
        {
            // Update quest progress berdasarkan categoryID dari infoItem
            QuestManager.Instance.OnInfoCollected(currentTrigger.infoItem.categoryID);
            InventoryInfoManager.Instance.AddInfo(currentTrigger.infoItem);

            Destroy(currentTrigger.gameObject);
            currentTrigger = null;
        }
    }

}
