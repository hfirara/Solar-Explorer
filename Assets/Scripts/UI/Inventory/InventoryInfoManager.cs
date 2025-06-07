using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InventoryInfoManager : MonoBehaviour
{
    public static InventoryInfoManager Instance { get; private set; }

    private HashSet<InfoItem> collectedInfos = new HashSet<InfoItem>();

    [Header("UI References")]
    [SerializeField] private GameObject panel;
    [SerializeField] private Transform contentParent;
    [SerializeField] private GameObject infoItemPrefab;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (panel.activeSelf)
                ClosePanel();
            else
                OpenPanel();
        }
    }

    public void AddInfo(InfoItem info)
    {
        if (!collectedInfos.Contains(info))
        {
            collectedInfos.Add(info);
            Debug.Log($"Info ditambahkan ke inventory: {info.name}");
        }
    }

    public bool HasInfo(InfoItem info)
    {
        return collectedInfos.Contains(info);
    }

    public IEnumerable<InfoItem> GetAllInfo()
    {
        return collectedInfos;
    }

    public void OpenPanel()
    {
        panel.SetActive(true);
        Time.timeScale = 0f;
        RefreshUI();
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void RefreshUI()
{
    foreach (Transform child in contentParent)
    {
        Destroy(child.gameObject);
    }

    foreach (InfoItem info in collectedInfos)
    {
        foreach (DataLine line in info.dataLines)
        {
            GameObject newItem = Instantiate(infoItemPrefab, contentParent);
            TMP_Text text = newItem.GetComponentInChildren<TMP_Text>();
            if (text != null)
                text.text = line.description;
        }
    }
}


}
