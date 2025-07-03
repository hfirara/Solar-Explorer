using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        // Ganti "NamaScene" dengan nama scene yang ingin kamu tuju
        SceneManager.LoadScene("Level 0 (Spaceship)");
    }

    public void QuitGame()
    {
        Debug.Log("Keluar dari game...");
        Application.Quit();
    }
}
