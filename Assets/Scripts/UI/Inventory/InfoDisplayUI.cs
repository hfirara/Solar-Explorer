using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoDisplayUI : MonoBehaviour
{
    public static InfoDisplayUI Instance { get; private set; }

    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ShowInfo(InfoItem info)
    {
        panel.SetActive(true);
        titleText.text = info.infoTitle;
        descriptionText.text = info.infoDescription;
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}
