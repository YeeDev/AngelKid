using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10;

    bool facingLeft;
    Vector2 moveInput;
    Rigidbody2D rb2D;


    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Run();
        CheckIfSpriteShouldFlip();
    }

    private void CheckIfSpriteShouldFlip()
    {
        if (moveInput.x > 0 && facingLeft) { FlipSprite(); }
        else if (moveInput.x < 0 && !facingLeft) { FlipSprite(); }
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        
    }

    private void Run()
    {
        Vector2 playerVelocity = moveInput * moveSpeed;
        playerVelocity.y = rb2D.velocity.y;

        rb2D.velocity = playerVelocity;
    }

    private void FlipSprite()
    {
        facingLeft = !facingLeft;

        Vector2 flippedScale = transform.localScale;
        flippedScale.x *= -1;
        transform.localScale = flippedScale;
    }
}
