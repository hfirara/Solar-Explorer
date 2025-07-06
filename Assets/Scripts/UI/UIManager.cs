using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private bool isDialogActive = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SetDialogActive(bool isActive)
    {
        isDialogActive = isActive;

        // Contoh: jika kamu ingin pause waktu saat dialog
        Time.timeScale = isActive ? 0f : 1f;

        // Atau kamu bisa juga memanggil method lain seperti:
        // PlayerController.Instance.SetCanMove(!isActive);
    }

    public bool IsDialogActive()
    {
        return isDialogActive;
    }

    public void PlayGame()
    {
        // Ganti "NamaScene" dengan nama scene yang ingin kamu tuju
        SceneManager.LoadScene("Menu");
    }
}
