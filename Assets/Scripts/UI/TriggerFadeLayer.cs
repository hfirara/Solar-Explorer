using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class TriggerFadeLayer : MonoBehaviour
{
    [SerializeField] private LayerFader layerFader;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            hasTriggered = true;
            layerFader.FadeAndLoadScene();
        }
    }
}
