using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 7.5f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float jumpTime = 0.5f;

    [Header("Ground check")]
    [SerializeField] private float extraHeight = 0.25f;
    [SerializeField] private LayerMask WhatIsGround;

    [Header("Health")]
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockbackForce = 5f;
    private int currentHealth;
    private bool isHurt = false;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    [Header("UI")]
    [SerializeField] public PlayerUI playerUI;

    [SerializeField] private float stepInterval = 0.4f;
    private float stepTimer = 0f;

    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;
    private float moveInput;

    private bool Jump;
    private float jumpTimeCounter;
    private RaycastHit2D groundHit;

    public static Player Instance;

    private Transform currentSafePoint;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();

        currentHealth = maxHealth;
        playerUI.UpdateHealthBar(currentHealth, maxHealth);

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        currentSafePoint = this.transform;
    }

    private void Update()
    {
        if (!this.enabled) return;

        moveInput = UserInput.instance.moveInput.x;
        Move();
        Jumping();
        UpdateAnimationState();

        float horizontal = Input.GetAxisRaw("Horizontal");
        bool isMoving = Mathf.Abs(horizontal) > 0.1f;

        // 🔥 Bunyi step satu kali langsung saat tap
        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow)) && IsGrounded())
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.run);
        }

        // 🔥 Kalau ditekan lama, bunyi berulang
        if (isMoving && IsGrounded())
        {
            stepTimer += Time.deltaTime;

            if (stepTimer >= stepInterval)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.run);
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = stepInterval; // reset supaya langsung bunyi pas mulai lagi
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput > 0 && transform.localScale.x < 0)
            Flip();
        else if (moveInput < 0 && transform.localScale.x > 0)
            Flip();
    }

    private void Jumping()
    {
        if (UserInput.instance.controls.Jumping.Jump.WasPressedThisFrame() && IsGrounded())
        {
            Jump = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            AudioManager.Instance.PlaySFX(AudioManager.Instance.jump);
        }

        if (UserInput.instance.controls.Jumping.Jump.IsPressed())
        {
            if (jumpTimeCounter > 0 && Jump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        if (UserInput.instance.controls.Jumping.Jump.WasReleasedThisFrame())
        {
            Jump = false;
        }
    }

    private void UpdateAnimationState()
    {
        if (!IsGrounded())
        {
            anim.SetBool("Jump", true);
            anim.SetBool("Run", false);
        }
        else
        {
            anim.SetBool("Jump", false);
            anim.SetBool("Run", Mathf.Abs(moveInput) > 0.01f);
        }
    }

    private bool IsGrounded()
    {
        groundHit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, extraHeight, WhatIsGround);
        return groundHit.collider != null;
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void TakeDamage(Vector2 damageDirection)
    {
        if (isHurt) return;

        AudioManager.Instance.PlaySFX(AudioManager.Instance.hit);

        currentHealth--;
        playerUI.UpdateHealthBar(currentHealth, maxHealth);

        isHurt = true;
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(-damageDirection.x * knockbackForce, knockbackForce), ForceMode2D.Impulse);

        StartCoroutine(FlashRedEffect());

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(RecoverFromHurt());
        }
    }

    private IEnumerator FlashRedEffect()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = originalColor;
    }

    private IEnumerator RecoverFromHurt()
    {
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.color = originalColor;
        isHurt = false;
    }

    public void Die()
    {
        this.enabled = false;
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;
        coll.enabled = false;

        GameManager.Instance.GameOver();
    }

    public void Respawn()
    {
        transform.position = currentSafePoint.position;

        currentHealth = maxHealth;
        playerUI.UpdateHealthBar(currentHealth, maxHealth);
        spriteRenderer.color = originalColor;

        isHurt = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = Vector2.zero;
        coll.enabled = true;
        this.enabled = true;
    }

    public void SetSafePoint(Transform newPoint)
    {
        currentSafePoint = newPoint;
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        playerUI.UpdateHealthBar(currentHealth, maxHealth);
        spriteRenderer.color = originalColor;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}