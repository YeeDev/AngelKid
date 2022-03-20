using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10;

    Vector2 moveInput;
    Rigidbody2D rb2D;


    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Run();
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
}
