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
    
    [HideInInspector] public bool IsFacingRight;

    [Header("Camera Stuff")]
    [SerializeField] private GameObject camera;
    private CameraFollow _cameraFollow;

    private Rigidbody2D rb;
    private Animator anim;
    private float moveInput;

    private bool Jump;
    private bool Fall;
    private float jumpTimeCounter;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        StartDirectionCheck();
    }

    private void Update()
    {
        Move();

        //if (Input.GetKey(KeyCode.Space) && jump)
        Jumping();
        
        
    }
    
    #region Movement Function

    private void Move()
    {
        moveInput = UserInput.instance.moveInput.x;
        
        if(moveInput > 0 || moveInput < 0)
        {
            anim.SetBool("Run", true);
            anim.SetBool("Jump", Jump);
            TurnCheck();
        }

        else
        {
            anim.SetBool("Run", false);
        }

        rb.velocity = new Vector2 (moveInput * moveSpeed, rb.velocity.y);
    }

    private void Jumping()
    {
        //button was just pushed this frame
        if(UserInput.instance.controls.Jumping.Jump.WasPressedThisFrame())
        {
            Jump = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
        }

        //button is being held
        if(UserInput.instance.controls.Jumping.Jump.IsPressed())
        {
            if(jumpTimeCounter > 0 && Jump)
            {
                rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }

            else
            {
                Jump = false;
            }
            
        }

        //button was released this frame
        if(UserInput.instance.controls.Jumping.Jump.WasReleasedThisFrame())
        {
            Jump = false;
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


}
