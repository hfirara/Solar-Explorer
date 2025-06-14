using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroKeyTrigger : MonoBehaviour
{
    [SerializeField] private GameObject interactionKeyUI;

    private bool isPlayerInRange = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            interactionKeyUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            interactionKeyUI.SetActive(false);
        }
    }
}
