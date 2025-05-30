using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public InfoItem infoData;

    public void OnInteract()
    {
        InfoInventoryManager.Instance.AddInfo(infoData);
        Destroy(gameObject); // atau gameObject.SetActive(false);
    }
}
