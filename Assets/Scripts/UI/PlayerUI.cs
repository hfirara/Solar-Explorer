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
    public Transform infoContainer; // tempat prefab info di-spawn
    public GameObject infoItemPrefab;

    [Header("Game Over")]
    [SerializeField] private GameObject gameOverPanel;

    private bool isGamePaused = false;

    // Update health bar
    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthBarFill.fillAmount = currentHealth / maxHealth;
    }

    public void ShowGameOverPanel()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    public void HideGameOverPanel()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    #region Pause Game
    public void PauseGame()
    {
        CloseAllUI();
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
        CloseAllUI();
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
    public void OpenInventory(string categoryID = "Merkurius")
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

    public void CloseInventory()
    {
        inventory.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }
    #endregion

    private void CloseAllUI()
    {
        pauseMenu.SetActive(false);
        questLogUI.SetActive(false);
        pickUI.SetActive(false);
        inventory.SetActive(false);
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public bool IsGamePaused()
    {
        return isGamePaused;
    }
}
