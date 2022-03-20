using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 15f;
    [SerializeField] LayerMask groundMask = 0;

    Rigidbody2D rb;
    Collider2D col;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        Vector2 vectorSpeed = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y);
        rb.velocity = vectorSpeed;
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && col.IsTouchingLayers(groundMask))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else if (Input.GetButtonUp("Jump"))
        {
            Vector2 haltSpeed = rb.velocity;
            haltSpeed.y *= 0.5f;
            rb.velocity = haltSpeed;
        }
    }
}
