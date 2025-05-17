using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Question UI")]
    public GameObject panelPertanyaan;
    public TMP_Text teksPertanyaan;
    public Button[] tombolJawaban;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void TampilkanPertanyaan(string soal, string[] jawaban, int indexBenar)
    {
        panelPertanyaan.SetActive(true);
        Time.timeScale = 0f;
        teksPertanyaan.text = soal;

        for (int i = 0; i < tombolJawaban.Length; i++)
        {
            if (i < jawaban.Length)
            {
                tombolJawaban[i].gameObject.SetActive(true);
                tombolJawaban[i].GetComponentInChildren<TMP_Text>().text = jawaban[i];
                int index = i; // avoid closure issue
                tombolJawaban[i].onClick.RemoveAllListeners();
                tombolJawaban[i].onClick.AddListener(() =>
                {
                    JawabanDipilih(index == indexBenar);
                });
            }
            else
            {
                tombolJawaban[i].gameObject.SetActive(false);
            }
        }
    }

    private void JawabanDipilih(bool benar)
    {
        Debug.Log(benar ? "✅ Jawaban benar!" : "❌ Jawaban salah!");

        panelPertanyaan.SetActive(false);
        Time.timeScale = 1f;
    }
}
