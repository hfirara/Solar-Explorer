using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    public static InteractionUI Instance { get; private set; }

    [SerializeField] private GameObject imageObject;
    [SerializeField] private TMPro.TMP_Text keyText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void Show(string message = "Tekan E")
    {
        imageObject.SetActive(true);
        if (keyText != null)
            keyText.text = message;
    }

    public void Hide()
    {
        imageObject.SetActive(false);
    }
}