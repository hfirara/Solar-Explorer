using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManagerData : MonoBehaviour
{
    public static UIManagerData Instance;

    private bool isDataActive = false; // ← Gunakan huruf kecil untuk field (konvensi C#)

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SetDataActive(bool isActive)
    {
        isDataActive = isActive;
        Time.timeScale = isActive ? 0f : 1f;
    }

    public bool GetIsDataActive() // ← Ganti nama method ini
    {
        return isDataActive;
    }
}
