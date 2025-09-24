using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool isGameOver = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.Space))
        {
            string currentScene = SceneManager.GetActiveScene().name;

            if (currentScene == "Level 9 (Going Home)")
            {
                // Kalau scene = Level9, restart scene
                RestartScene();
            }
            else
            {
                // Kalau scene lain, respawn
                RespawnPlayer();
            }
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0f;

        AudioManager.Instance.PlaySFX(AudioManager.Instance.death);
        Player.Instance.playerUI.ShowGameOverPanel();
        isGameOver = true;
    }

    public void RespawnPlayer()
    {
        Debug.Log("Respawn Player");
        Time.timeScale = 1f;

        // Panggil Respawn() yang sudah pakai currentSafePoint
        Player.Instance.Respawn();

        Player.Instance.playerUI.HideGameOverPanel();
        isGameOver = false;
    }

    private void RestartScene()
    {
        Debug.Log("Restart Scene");
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isGameOver = false;
    }
}
