using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoInventoryUI : MonoBehaviour
{
    [Header("Panel Info Besar")]
    public GameObject planetListPanel;
    public Transform planetListContent;
    public GameObject planetButtonPrefab;

    [Header("Panel Info Kecil")]
    public GameObject planetDetailPanel;
    public TMP_Text planetTitleText;
    public Transform detailContent;
    public GameObject infoItemPrefab;

    private Dictionary<string, List<InfoItem>> planetInfos;

    void Start()
    {
        planetInfos = InfoInventoryManager.Instance.GetAllInfosGroupedByPlanet();
        ShowPlanetList();
    }

    public void ShowPlanetList()
    {
        planetListPanel.SetActive(true);
        planetDetailPanel.SetActive(false);

        Time.timeScale = 0f;

        foreach (Transform child in planetListContent)
        {
            Destroy(child.gameObject);
        }

        foreach (var kvp in planetInfos)
        {
            string planetName = kvp.Key;

            GameObject btn = Instantiate(planetButtonPrefab, planetListContent);
            btn.GetComponentInChildren<TMP_Text>().text = planetName;

            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                ShowPlanetDetail(planetName);
            });
        }
    }

    public void ShowPlanetDetail(string planetName)
    {
        planetListPanel.SetActive(false);
        planetDetailPanel.SetActive(true);

        Time.timeScale = 0f;

        planetTitleText.text = "Informasi Planet " + planetName;

        foreach (Transform child in detailContent)
        {
            Destroy(child.gameObject);
        }

        List<InfoItem> infoList = planetInfos[planetName];
        foreach (var info in infoList)
        {
            GameObject item = Instantiate(infoItemPrefab, detailContent);
            TMP_Text[] texts = item.GetComponentsInChildren<TMP_Text>();
            texts[0].text = info.title;
            texts[1].text = info.description;
        }
    }

    public void BackToPlanetList()
    {
        planetDetailPanel.SetActive(false);
        planetListPanel.SetActive(true);
    }

    public void CloseInventory()
    {
        planetDetailPanel.SetActive(false);
        planetListPanel.SetActive(false);

        Time.timeScale = 1f; // Resume game karena panel ditutup
    }
}
