using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[RequireComponent(typeof(Button))]
public class UIButtonSound : MonoBehaviour
{
    [Tooltip("Kosongkan jika ingin pakai default buttonClick dari AudioManager")]
    public AudioClip customClip;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(PlayClickSound);
    }

    private void PlayClickSound()
{
    if (AudioManager.Instance == null)
    {
        Debug.LogWarning("[SFX] AudioManager belum ada di scene.");
        return;
    }

    AudioClip clipToPlay = customClip != null ? customClip : AudioManager.Instance.buttonClick;

    if (clipToPlay != null)
    {
        AudioManager.Instance.PlaySFX(clipToPlay);
        Debug.Log($"[SFX] Memainkan suara: {clipToPlay.name}");
    }
    else
    {
        Debug.LogWarning("[SFX] Tidak ada AudioClip yang dipilih.");
    }
}

}
