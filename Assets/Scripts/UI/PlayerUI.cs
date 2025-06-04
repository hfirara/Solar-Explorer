using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image healthBarFill;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject questLogUI;
    [SerializeField] private GameObject pickUI;
    
    public GameObject gameOverPanel;

    private bool isGamePaused = false;

    // Update health bar
    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthBarFill.fillAmount = currentHealth / maxHealth;
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    #region Pause Game
    public void PauseGame()
    {
        CloseAllUI(); // pastikan UI lain ditutup
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }
    #endregion

    #region Quest Log
    public void OpenQuestLog()
    {
        CloseAllUI(); // pastikan UI lain ditutup
        questLogUI.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void CloseQuestLog()
    {
        questLogUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }
    #endregion

    #region Pick UI

    public void ClosePick()
    {
        pickUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }
    #endregion

    // Optional: Tutup semua UI sebelum buka salah satu
    private void CloseAllUI()
    {
        pauseMenu.SetActive(false);
        questLogUI.SetActive(false);
        pickUI.SetActive(false);
    }

    // Untuk pengecekan eksternal (misal: Player.cs ingin tahu status pause)
    public bool IsGamePaused()
    {
        return isGamePaused;
    }
}
