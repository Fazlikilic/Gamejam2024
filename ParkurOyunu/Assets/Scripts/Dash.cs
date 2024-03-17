using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dash : MonoBehaviour
{
    //move
    private Rigidbody2D _rigidbody2d;
    [SerializeField] float hiz;
    private float move;
    //jump
    private bool platform;
    [SerializeField] Transform platformCheck;
    public LayerMask platformuAta;
    [SerializeField] float jumpForce;
    private bool grounded = true;
    //dash
    private bool canDash = true;
    private bool isDashing;
    [SerializeField]private float dashAmount = 25f;
    private float dashTime = 0.2f;
    private float dashCooldown = 1.0f;
    //animation
    private bool jump;
    private Animator anim;
    private float speed = 1.0f;
    private float moveDirection;
    SpriteRenderer _spriteRenderer;
    [SerializeField] private TrailRenderer tr;

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        move = Input.GetAxis("Horizontal");
        _rigidbody2d.velocity = new Vector2(move * hiz, _rigidbody2d.velocity.y);
        platform = Physics2D.OverlapCircle(platformCheck.position, 0.2f, platformuAta);
    }

    private void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        anim.SetFloat("speed", speed);

        if(speed < 0.0f)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
        if (grounded == true)
        {
            moveDirection = 0.0f;
            anim.SetFloat("speed", 0.0f);
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded == true)
        {
            _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, jumpForce);
            jump = true;
            grounded = false;
            anim.SetFloat("speed", 1.0f);
            anim.SetTrigger("jump");
            anim.SetBool("grounded", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash == true)
        {
            StartCoroutine(dash());
            Debug.Log("dash atýlýyor");
        }
    }

    IEnumerator dash()
    {
        canDash = false;
        isDashing = true;
        _rigidbody2d.gravityScale = 0f;
        _rigidbody2d.velocity = new Vector2(move * dashAmount, _rigidbody2d.velocity.y);
        tr.emitting = true;
        yield return new WaitForSeconds(dashTime);
        tr.emitting = false;
        _rigidbody2d.gravityScale = 1.0f;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    private void OnCollisionEnter2D(Collision2D other) //baðlý olduðum obje baþka collision objeye çarpýyorsa bu event çalýþýr
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("grounded", true);
            grounded = true;
        }
    }
}
