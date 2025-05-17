using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float verticalLimit = 5f;

    private Rigidbody2D rb;
    private bool isAlive = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // Tanpa gravitasi
    }

    private void Update()
    {
        if (!isAlive) return;

        float verticalInput = Input.GetAxisRaw("Vertical"); // W/S atau Arrow Up/Down
        Vector2 movement = new Vector2(0f, verticalInput * moveSpeed);

        rb.velocity = movement;

        // Batasi posisi vertikal jika perlu (opsional)
        float clampedY = Mathf.Clamp(transform.position.y, -verticalLimit, verticalLimit);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isAlive = false;
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;

        GameManager.Instance.GameOver();
    }
}
