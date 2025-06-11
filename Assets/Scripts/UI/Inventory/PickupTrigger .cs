using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickTrigger : MonoBehaviour
{
    public InfoItem infoItem;

    private bool isPlayerInRange = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            PickManager.Instance.ShowInteractKey(true);
        }
    }

    /*private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            // Pastikan tetap tampil selama player di dalam trigger
            PickManager.Instance.ShowInteractKey(true);
        }
    }*/

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            PickManager.Instance.ShowInteractKey(false);
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && !PickManager.Instance.IsDataRunning)
        {
            PickManager.Instance.StartData(infoItem, this);
        }
    }
}
