using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoDisplayUI : MonoBehaviour
{
    public static InfoDisplayUI Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private GameObject dataPanel;
    [SerializeField] private TMP_Text dataText;
    [SerializeField] private Button closeButton;

    [Header("Data Content")]
    [TextArea]
    public string[] dataLines;

    private int currentLine = 0;

    /*private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }*/

    private void Start()
    {
        dataPanel.SetActive(false);
    }

    /*public void ShowInfo(InfoItem info)
    {
        panel.SetActive(true);
        titleText.text = info.infoTitle;
        descriptionText.text = info.infoDescription;
    }*/

    public void StartDialog()
    {
        currentLine = 0;
        dataPanel.SetActive(true);
        dataText.text = dataLines[currentLine];
    }

    /*public void Hide()
    {
        panel.SetActive(false);
    }*/
}
