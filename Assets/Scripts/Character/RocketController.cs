using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class RocketController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float verticalLimit = 8f;

    [Header("Health")]
    [SerializeField] private int maxHealth = 5;
    [SerializeField] public PlayerUI playerUI;
    [SerializeField] private float hurtDuration = 0.2f;
    [SerializeField] private float shakeAmount = 0.1f;
    [SerializeField] private float shakeDuration = 0.2f;

    private int currentHealth;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool isAlive = true;
    private bool isHurt = false;
    private bool isDodging = false;
    private Animator anim;

    public static RocketController Instance;

    AudioManager audioManager;

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        rb.gravityScale = 0f;

        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;

        if (playerUI != null)
            playerUI.UpdateHealthBar(currentHealth, maxHealth);
    }

    private void Update()
    {
        if (!isAlive && Input.GetKeyDown(KeyCode.Space))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }

        if (!isAlive || isDodging) return;

        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(0f, verticalInput * moveSpeed);
        rb.velocity = movement;

        float clampedY = Mathf.Clamp(transform.position.y, -verticalLimit, verticalLimit);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isHurt || !isAlive) return;

        TakeDamage();
    }

    public void TakeDamage()
    {
        currentHealth--;

        if (playerUI != null)
            playerUI.UpdateHealthBar(currentHealth, maxHealth);

        StartCoroutine(FlashRed());
        StartCoroutine(Shake());

        if (currentHealth <= 0)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.death);
            Die();
        }
    }

    private IEnumerator FlashRed()
    {
        isHurt = true;
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(hurtDuration);

        spriteRenderer.color = Color.white;
        isHurt = false;
    }

    private IEnumerator Shake()
    {
        Vector3 originalPos = transform.position;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float offsetX = Random.Range(-shakeAmount, shakeAmount);
            float offsetY = Random.Range(-shakeAmount, shakeAmount);

            transform.position = originalPos + new Vector3(offsetX, offsetY, 0f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos;
    }

    private void Die()
    {
        isAlive = false;

        anim.SetBool("isDead", true);

        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1.5f;

        StartCoroutine(ShowGameOverAfterDelay());
    }

    private IEnumerator ShowGameOverAfterDelay()
    {
        yield return new WaitForSeconds(1f);

        if (playerUI != null)
            playerUI.ShowGameOverPanel();
    }

    public void Dodge(Vector2 direction, float distance = 2f, float duration = 0.4f)
    {
        if (!isAlive || isDodging) return;
        StartCoroutine(DodgeRoutine(direction, distance, duration));
    }

    private IEnumerator DodgeRoutine(Vector2 direction, float distance, float duration)
    {
        isDodging = true;
        rb.velocity = Vector2.zero;

        Vector3 start = transform.position;
        Vector3 offset = (Vector3)(direction.normalized * distance);
        
        // Tambahan: sedikit geser ke depan juga saat menghindar
        offset += new Vector3(0f, 0f, 0f); 

        Vector3 target = start + offset;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(start, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = target;
        isDodging = false;
    }

    public void LaunchForward(float speed)
    {
        rb.velocity = Vector2.right * speed;
    }

}
