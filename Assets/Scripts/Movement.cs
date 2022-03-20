using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 15f;
    [SerializeField] float climbSpeed = 2f;
    [SerializeField] LayerMask jumpableMask = 0;
    [SerializeField] LayerMask climbableMask = 0;
    [SerializeField] float ladderOffset = 0.5f;

    bool isClimbing;
    float initialGravity;
    Rigidbody2D rb;
    Collider2D col;
    Collider2D ladder;
    EdgeCollider2D ladderTop;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        initialGravity = rb.gravityScale;
    }

    private void Update()
    {
        StartClimb();
        Climb();
        Move();
        Jump();

        Debug.Log(col.IsTouchingLayers(LayerMask.GetMask("Climbable")));
    }

    private void Climb()
    {
        if (!isClimbing) { return; }

        //TODO Use Animator Movement Instead (?)
        rb.velocity = new Vector2(0, Input.GetAxisRaw("Vertical") * climbSpeed);

        if (col.IsTouchingLayers(LayerMask.GetMask("Ground")) && Input.GetAxisRaw("Vertical") < 0)
        {
            ResetClimbSettings();
        }

        if (ladder.bounds.max.y + ladderOffset < col.bounds.min.y) { ResetClimbSettings(); }
    }

    private void StartClimb()
    {
        if (col.IsTouchingLayers(LayerMask.GetMask("Climbable")))
        {
            if (Input.GetButtonDown("Vertical") && !isClimbing)
            {
                isClimbing = true;
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
                ladder = Physics2D.OverlapCircle(transform.position, 1, climbableMask);
                ladderTop = ladder.transform.GetComponentInChildren<EdgeCollider2D>();
                ladderTop.enabled = false;
                transform.position = new Vector2(ladder.transform.position.x, transform.position.y);
            }
        }
    }

    private void Move()
    {
        if (isClimbing) { return; }

        Vector2 vectorSpeed = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y);
        rb.velocity = vectorSpeed;
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && col.IsTouchingLayers(jumpableMask))
        {
            ResetClimbSettings();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else if (Input.GetButtonUp("Jump"))
        {
            Vector2 haltSpeed = rb.velocity;
            haltSpeed.y *= 0.5f;
            rb.velocity = haltSpeed;
        }
    }

    private void ResetClimbSettings()
    {
        isClimbing = false;
        rb.gravityScale = initialGravity;
        if(ladderTop != null)
            ladderTop.enabled = true;
    }
}
