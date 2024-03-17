using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    [SerializeField] public float jumpForce = 20.0f;
    [SerializeField] public float speed = 1.0f;
    private float moveDirection;
    private bool jump;
    private Animator anim;
    private bool moving;
    private bool grounded = true;
    private bool doubleJump;
    private Rigidbody2D _rigidbody2d;
    SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (_rigidbody2d.velocity != Vector2.zero)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }

        _rigidbody2d.velocity = new Vector2(speed * moveDirection, _rigidbody2d.velocity.y);

        if (jump == true)
        {
            _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, jumpForce);
            jump = false;
        }
        if (doubleJump == true)
        {
            _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, jumpForce);
            doubleJump = false;
        }
    }

    private void Update()
    {
        if (grounded == true && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            if (Input.GetKey(KeyCode.A))
            {
                moveDirection = -1.0f;
                _spriteRenderer.flipX = true;
                anim.SetFloat("speed", speed);
                _spriteRenderer.flipX = false;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                moveDirection = 1.0f;
                _spriteRenderer.flipY = false;
                anim.SetFloat("speed", speed);
                _spriteRenderer.flipX = true;
            }
        }
        else if (grounded == true)
        {
            moveDirection = 0.0f;
            anim.SetFloat("speed", 0.0f);
        }

        if (grounded == true && !Input.GetKey(KeyCode.Space))
        {
            doubleJump = false;
        }

        if (grounded == true && Input.GetKey(KeyCode.Space))
        {
            jump = true;
            grounded = false;
            anim.SetTrigger("jump");
            anim.SetBool("grounded", false);
            if (grounded == false || doubleJump == true)
            {
                doubleJump = true;
                doubleJump = !doubleJump;
                grounded = false;
            }

        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("grounded", true);
            grounded = true;
        }
    }
}
