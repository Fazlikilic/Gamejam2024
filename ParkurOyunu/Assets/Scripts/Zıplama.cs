using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Zıplama : MonoBehaviour
{
    private Rigidbody2D _rigidbody2d;
    private SpriteRenderer _spriteRenderer;
    private bool grounded = true;
    private bool jump;
    [SerializeField] float jumpForce = 20.0f;
    [SerializeField] float speed = 4.0f;


    private void Start()
    {
        grounded = true;
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (jump == true)
        {
            _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, jumpForce);
            jump = false;
        }
    }

    private void Update()
    {
        _rigidbody2d.freezeRotation = true;
        if (grounded == true && (Input.GetKey(KeyCode.Space)))
        {
            grounded = false;
            _rigidbody2d.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x,jumpForce);
            jumpForce = -jumpForce;
        }
        if(Input.GetKey(KeyCode.A)) 
        {
            _spriteRenderer.flipX = false;
            _rigidbody2d.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            _rigidbody2d.velocity = new Vector2(-speed, _rigidbody2d.velocity.y);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _spriteRenderer.flipX = true;
            _rigidbody2d.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            _rigidbody2d.velocity = new Vector2(speed, _rigidbody2d.velocity.y);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            _rigidbody2d.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
    }
}
