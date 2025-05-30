using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoInventoryManager : MonoBehaviour
{
     public static InfoInventoryManager Instance { get; private set; }

    private List<InfoItem> collectedInfos = new List<InfoItem>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddInfo(InfoItem info)
    {
        if (!collectedInfos.Contains(info))
        {
            collectedInfos.Add(info);
        }
    }

    public List<InfoItem> GetAllInfo()
    {
        return collectedInfos;
    }

    public Dictionary<string, List<InfoItem>> GetAllInfosGroupedByPlanet()
    {
        Dictionary<string, List<InfoItem>> grouped = new Dictionary<string, List<InfoItem>>();

        foreach (var info in collectedInfos)
        {
            string planet = info.categoryID;

            if (!grouped.ContainsKey(planet))
                grouped[planet] = new List<InfoItem>();

            grouped[planet].Add(info);
        }

        return grouped;
    }

}
