using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool jump;

    [Header("Camera Stuff")]
    [SerializeField] private GameObject camera;
    private CameraFollow _cameraFollow;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        _cameraFollow = camera.GetComponent<CameraFollow>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
            _cameraFollow.CallTurn();
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            _cameraFollow.CallTurn();
        }
        if(Input.GetKey(KeyCode.Space) && jump)
            Jump();

        anim.SetBool ("Run", horizontalInput != 0);
        anim.SetBool ("Jump", jump);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("High");
        jump = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
            jump = true;
    }
}