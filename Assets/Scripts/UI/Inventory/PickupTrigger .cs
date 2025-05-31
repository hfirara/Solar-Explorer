using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupTrigger : MonoBehaviour
{
    public InfoItem infoItem; // drag ScriptableObject InfoItem ke sini
    private bool isPlayerInRange = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            InteractionUI.Instance.Show("Tekan E");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            InteractionUI.Instance.Hide();
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            InfoDisplayUI.Instance.ShowInfo(infoItem); // tampilkan panel info
            InteractionUI.Instance.Hide(); // sembunyikan tombol E
            Destroy(gameObject); // hilangkan objek dari scene
        }
    }
}