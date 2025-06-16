using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class TriggerFadeScene : MonoBehaviour
{
    [SerializeField] private SceneFader sceneFader; // Drag di Inspector

    private bool hasTriggered = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("Trigger masuk! Memanggil fade...");
            hasTriggered = true;
            sceneFader.FadeAndLoadScene(); // ← panggil method dari SceneFader
        }
    }
}