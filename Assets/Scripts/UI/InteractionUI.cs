using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private GameObject interactionUI;
    [SerializeField] private TMP_Text promptText;

    /*private void Start()
    {
        if (interactionUI != null)
            interactionUI.SetActive(false);
    }*/

    public void Show(string message)
    {
        if (interactionUI != null)
            interactionUI.SetActive(true);

        if (promptText != null)
            promptText.text = message;
    }

    public void Hide()
    {
        if (interactionUI != null)
            interactionUI.SetActive(false);
    }
}