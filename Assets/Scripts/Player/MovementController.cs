using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;


public class MovementController : MonoBehaviour
{
    [SerializeField]
    private int multiJumpCount = 1;
    [SerializeField]
    private float movementSpeed = 5f;
    [SerializeField]
    private float jumpForce = 10f;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rigidBody;
    private int jumpCount = 0;
    private float horizontalInput;
    private float verticalInput;
    private AudioSource src;
    public AudioClip jump;
    public AudioClip walk;

    private bool isInAir = false;

    public event System.Action OnJump;
    public event System.Action OnLand;
    public event System.Action OnMove;
    public event System.Action OnStop;
    public event System.Action OnFall;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        src = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(horizontalInput * movementSpeed, rigidBody.velocity.y);


        if (!IsGrounded()) //for jump cancelation
        {
            OnFall?.Invoke();
        }

        if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        bool wasInAir = isInAir;
        isInAir = !IsGrounded();

        if (wasInAir && !isInAir)
        {
            jumpCount = 0;
            OnLand?.Invoke();
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.2f, LayerMask.GetMask("Obstacle"));
    }

    public void Move(InputAction.CallbackContext context)
    {
        src.clip = walk;
        horizontalInput = context.ReadValue<Vector2>().x;

        if (horizontalInput == 0)
        {
            OnStop?.Invoke();
            return;
        }
        src.Play();
        OnMove?.Invoke();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            src.clip = jump;
            src.Play();
            return;
        }
        if (jumpCount == multiJumpCount)
        {
            src.clip = jump;
            src.Play();
            return;
        }
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0); //reset y velocity so the jump is from a "standing position"
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        jumpCount++;
        OnJump?.Invoke();
    }
}
