using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private GameObject uiObject;
    [SerializeField] private TMP_Text keyText;

    /*private void Start()
    {
        if (interactionUI != null)
            interactionUI.SetActive(false);
    }

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
    }*/

    public void Show(string key, Vector3 worldPosition)
    {
        keyText.text = key;
        uiObject.SetActive(true);
        transform.position = worldPosition;
    }

    public void Hide()
    {
        uiObject.SetActive(false);
    }
}