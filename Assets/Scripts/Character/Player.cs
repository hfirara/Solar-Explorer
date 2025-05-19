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

    [Header("Camera Stuff")]
    [SerializeField] private GameObject camera;
    private CameraFollow _cameraFollow;

    [Header("Health")]
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockbackForce = 5f;
    private int currentHealth;
    private bool isHurt = false;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    [Header("UI")]
    [SerializeField] private PlayerUI playerUI;

    [HideInInspector] public bool IsFacingRight;

    private Rigidbody2D rb;
    private Animator anim;
    private float moveInput;

    private bool Jump;
    private bool Fall;
    private float jumpTimeCounter;
    
    private Collider2D coll;
    private RaycastHit2D groundHit;

    public static Player Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy (gameObject);
        }

        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();

        StartDirectionCheck();

        _cameraFollow = camera.GetComponent<CameraFollow>();

        currentHealth = maxHealth;
        playerUI.UpdateHealthBar(currentHealth, maxHealth);
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void Update()
    {
         if (DialogManager.Instance != null && DialogManager.Instance.IsDialogActive)
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("Run", false);
            return;
        }
        //if (Input.GetKey(KeyCode.Space) && jump)
        Move();
        Jumping();
        UpdateAnimationState();
    }
    
    #region Movement Function

    private void Move()
    {
        moveInput = UserInput.instance.moveInput.x;

        TurnCheck();
        
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (isHurt) return;
    }

    private void Jumping()
    {
        //button was just pushed this frame
        if(UserInput.instance.controls.Jumping.Jump.WasPressedThisFrame() && IsGrounded())
        {
            Jump = true;
            jumpTimeCounter = jumpTime;
            //anim.SetBool("Jump", true);
            rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
        }

        //button is being held
        if(UserInput.instance.controls.Jumping.Jump.IsPressed())
        {
            if(jumpTimeCounter > 0 && Jump)
            {
                rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
                //anim.SetTrigger("High");
                jumpTimeCounter -= Time.deltaTime;
            }

            else if (jumpTimeCounter == 0)
            {
                Jump = false;
            }
            
        }

        //button was released this frame
        if(UserInput.instance.controls.Jumping.Jump.WasReleasedThisFrame())
        {
            Jump = false;
        }

        DrawGroundCheck();

        if (isHurt) return;
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

            if (Mathf.Abs(moveInput) > 0.01f)
            {
                anim.SetBool("Run", true);
            }
            else
            {
                anim.SetBool("Run", false);
            }
        }
    }

    #endregion

    #region Ground Check

    private bool IsGrounded()
    {
        groundHit = Physics2D.BoxCast (coll.bounds.center, coll.bounds.size, 0f, Vector2.down, extraHeight, WhatIsGround);

        if (groundHit.collider != null)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    #endregion

    #region Turn Checks

    private void StartDirectionCheck()
    {
        IsFacingRight = transform.localScale.x > 0f;
    }

    private void TurnCheck()
    {
        if (UserInput.instance.moveInput.x > 0 && !IsFacingRight)
        {
            Turn();
        }
        
        else if (UserInput.instance.moveInput.x < 0 && IsFacingRight)
        {
            Turn();
        }
    }

    private void Turn()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        
        IsFacingRight = scale.x > 0f;
    }

    #endregion

    #region Debug Function

    private void DrawGroundCheck()
    {
        Color rayColor;

        if (IsGrounded())
        {
            rayColor = Color.green;
        }

        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay (coll.bounds.center + new Vector3 (coll.bounds.extents.x, 0), Vector2.down * (coll.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay (coll.bounds.center - new Vector3 (coll.bounds.extents.x, 0), Vector2.down * (coll.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay (coll.bounds.center - new Vector3 (coll.bounds.extents.x, coll.bounds.extents.y + extraHeight), Vector2.right * (coll.bounds.extents.x + 2), rayColor);
    }

    #endregion

    #region Healt function

    public void TakeDamage(Vector2 damageDirection)
    {
        if (isHurt) return;

        currentHealth--;
        playerUI.UpdateHealthBar(currentHealth, maxHealth);

        isHurt = true;
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(-damageDirection.x * knockbackForce, knockbackForce), ForceMode2D.Impulse);

        anim.SetTrigger("Damage");

        // Ubah warna jadi merah
        spriteRenderer.color = Color.red;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(RecoverFromHurt());
        }
    }

    private IEnumerator RecoverFromHurt()
    {
        yield return new WaitForSeconds(0.5f);

        // Kembalikan warna ke semula
        spriteRenderer.color = originalColor;
        isHurt = false;
    }


    private void Die()
    {
        anim.SetTrigger("Die");
        this.enabled = false;
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;
    }

    #endregion 
}
