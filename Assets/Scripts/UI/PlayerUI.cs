using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image healthBarFill;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject questLogUI;
    [SerializeField] private GameObject pickUI;
    [SerializeField] private GameObject inventory;

    [Header("Inventory")]
    public GameObject inventoryPanel;
    public Transform infoContainer; // tempat prefab info di-spawn
    public GameObject infoItemPrefab;
    
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

    #region Inventory
    public void OpenInventory(string categoryID)
    {
        CloseAllUI();
        inventory.SetActive(true);

        foreach (Transform child in infoContainer)
        {
            Destroy(child.gameObject);
        }

        var infoList = InventoryInfoManager.Instance.GetInfoByCategory(categoryID);
        foreach (var info in infoList)
        {
            foreach (var line in info.dataLines)
            {
                GameObject item = Instantiate(infoItemPrefab, infoContainer);
                TMP_Text text = item.GetComponentInChildren<TMP_Text>();
                if (text != null)
                    text.text = line.description;
            }
        }

        Time.timeScale = 0f;
        isGamePaused = true;
    }


    public void ClosePanel()
    {
        inventory.SetActive(false);
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
        inventory.SetActive(false);
    }

    // Untuk pengecekan eksternal (misal: Player.cs ingin tahu status pause)
    public bool IsGamePaused()
    {
        return isGamePaused;
    }
}
