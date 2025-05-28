using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[RequireComponent(typeof(Button))]
public class UIButtonSound : MonoBehaviour
{
    [Tooltip("Kosongkan untuk menggunakan default AudioManager buttonClick")]
    public AudioClip customClip;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(PlayClickSound);
    }

    private void PlayClickSound()
    {
        AudioClip clipToPlay = customClip != null ? customClip : AudioManager.Instance.buttonClick;
        AudioManager.Instance.PlaySFX(clipToPlay);

        if (clipToPlay != null)
        {
            Debug.Log($"[SFX] Memainkan suara: {clipToPlay.name}");
        }
        else
        {
            Debug.LogWarning("[SFX] Tidak ada AudioClip yang dipilih untuk tombol ini.");
        }
    }
}
