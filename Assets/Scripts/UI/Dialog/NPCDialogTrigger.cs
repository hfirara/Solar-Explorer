using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogTrigger : MonoBehaviour
{
    public DialogData dialogData;

    private bool playerInRange = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            DialogManager.Instance.ShowInteractKey(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            DialogManager.Instance.ShowInteractKey(false);
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            DialogManager.Instance.StartDialog(dialogData);
        }
    }
}
