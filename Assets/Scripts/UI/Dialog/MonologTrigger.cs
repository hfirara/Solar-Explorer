using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonologTrigger : MonoBehaviour
{
    public DialogData monologData;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            DialogManager.Instance.StartDialog(monologData);
            triggered = true;
            gameObject.SetActive(false); // opsional: nonaktifkan trigger biar gak bisa dipicu ulang
        }
    }
}
