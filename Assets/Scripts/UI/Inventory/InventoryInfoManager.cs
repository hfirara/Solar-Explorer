using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InventoryInfoManager : MonoBehaviour
{
    public static InventoryInfoManager Instance { get; private set; }

    // Info dikategorikan berdasarkan categoryID (misal: "Merkurius", "Venus", dll)
    private Dictionary<string, List<InfoItem>> categorizedInfos = new Dictionary<string, List<InfoItem>>();

    [Header("UI References")]
    [SerializeField] private GameObject panel;
    [SerializeField] private Transform contentParent;
    [SerializeField] private GameObject infoItemPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddInfo(InfoItem info)
    {
        if (!categorizedInfos.ContainsKey(info.categoryID))
        {
            categorizedInfos[info.categoryID] = new List<InfoItem>();
        }

        if (!categorizedInfos[info.categoryID].Contains(info))
        {
            categorizedInfos[info.categoryID].Add(info);
            Debug.Log($"Info ditambahkan ke kategori {info.categoryID}: {info.name}");
        }
    }

    public void OnCategoryButtonClicked(string categoryID)
    {
        ShowInfoByCategory(categoryID);
    }

    public bool HasInfo(InfoItem info)
    {
        if (categorizedInfos.TryGetValue(info.categoryID, out List<InfoItem> list))
        {
            return list.Contains(info);
        }
        return false;
    }

    public IEnumerable<InfoItem> GetAllInfo()
    {
        List<InfoItem> allInfos = new List<InfoItem>();
        foreach (var kvp in categorizedInfos)
        {
            allInfos.AddRange(kvp.Value);
        }
        return allInfos;
    }

    public void OpenPanel()
    {
        panel.SetActive(true);
        Time.timeScale = 0f;
        ShowInfoByCategory("Merkurius"); // Ganti dengan default kamu
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
    }

    // Tampilkan kategori pertama secara default
    public void ShowInfoByCategory()
    {
        if (categorizedInfos.Count > 0)
        {
            foreach (var key in categorizedInfos.Keys)
            {
                ShowInfoByCategory(key);
                break;
            }
        }
    }

    // Tampilkan info berdasarkan kategori tertentu
    public void ShowInfoByCategory(string categoryID)
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        List<InfoItem> infoList = GetInfoByCategory(categoryID);

        foreach (InfoItem info in infoList)
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

    public List<InfoItem> GetInfoByCategory(string categoryID)
    {
        if (categorizedInfos.ContainsKey(categoryID))
            return categorizedInfos[categoryID];
        return new List<InfoItem>();
    }

}
