using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float thrustForce = 5f;
    [SerializeField] private float maxYVelocity = 10f;

    private Rigidbody2D rb;
    private bool isAlive = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!isAlive) return;

        // Tap / click untuk naik
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            ThrustUp();
        }
    }

    private void ThrustUp()
    {
        // Reset kecepatan vertikal sebelum dorongan baru
        rb.velocity = new Vector2(rb.velocity.x, 0f);

        // Tambahkan gaya dorong ke atas
        rb.AddForce(Vector2.up * thrustForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Saat menabrak sesuatu = mati
        isAlive = false;
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;

        // Beritahu GameManager (nanti kita buat)
        GameManager.Instance.GameOver();
    }
}
