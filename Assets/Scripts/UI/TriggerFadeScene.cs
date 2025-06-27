using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class TriggerFadeScene : MonoBehaviour
{
    [SerializeField] private SceneFader sceneFader; // Drag dari Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player masuk trigger, cek syarat fade...");

            // Langsung panggil FadeAndLoadScene
            sceneFader.FadeAndLoadScene(); 
        }
    }
}