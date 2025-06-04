using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickTrigger : MonoBehaviour
{
    public InfoItem infoItem;

    private bool isPlayerInRange = false;

    /*private void Start()
    {
        // Cek manual apakah player sudah berada di dalam trigger saat game dimulai
        Collider2D player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Collider2D>();
        Collider2D trigger = GetComponent<Collider2D>();

        if (player != null && trigger != null && trigger.IsTouching(player))
        {
            isPlayerInRange = true;
            PickManager.Instance.ShowInteractKey(true);
        }
    }*/

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            PickManager.Instance.ShowInteractKey(true);
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && !PickManager.Instance.IsDataRunning)
        {
            PickManager.Instance.StartData(infoItem, this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            PickManager.Instance.ShowInteractKey(false);
        }
    }

}
