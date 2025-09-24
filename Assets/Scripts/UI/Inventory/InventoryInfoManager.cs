using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InventoryInfoManager : MonoBehaviour
{
    public static InventoryInfoManager Instance { get; private set; }

    private Dictionary<string, List<InfoItem>> categorizedInfos = new Dictionary<string, List<InfoItem>>();

    [Header("UI References")]
    [SerializeField] private GameObject panel;
    [SerializeField] private Transform contentParent;
    [SerializeField] private GameObject infoItemPrefab;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // penting untuk antar scene
    }

    public void AddInfo(InfoItem info)
    {
        if (info == null || string.IsNullOrEmpty(info.categoryID)) return;

        if (!categorizedInfos.ContainsKey(info.categoryID))
        {
            categorizedInfos[info.categoryID] = new List<InfoItem>();
        }

        if (!categorizedInfos[info.categoryID].Contains(info))
        {
            categorizedInfos[info.categoryID].Add(info);
            Debug.Log($"[Inventory] Info ditambahkan ke kategori {info.categoryID}: {info.name}");
            UINotification.Instance.ShowNotification("Info baru ditambahkan!");
        }
    }

    public bool HasInfo(InfoItem info)
    {
        if (info == null || !categorizedInfos.TryGetValue(info.categoryID, out List<InfoItem> list))
            return false;

        return list.Contains(info);
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

    public List<InfoItem> GetInfoByCategory(string categoryID)
    {
        if (categorizedInfos.ContainsKey(categoryID))
            return categorizedInfos[categoryID];
        return new List<InfoItem>();
    }

    public void OnCategoryButtonClicked(string categoryID)
    {
        ShowInfoByCategory(categoryID);
    }

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

    public void ShowInfoByCategory(string categoryID)
    {
        if (contentParent == null || infoItemPrefab == null) return;

        // Bersihkan UI lama
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // Tampilkan info berdasarkan kategori
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

    public int GetInfoCountByCategory(string categoryID)
    {
        if (categorizedInfos.ContainsKey(categoryID))
        {
            return categorizedInfos[categoryID].Count;
        }
        return 0;
    }
}