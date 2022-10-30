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

    //animator variables
    [SerializeField]
    Animator anim;
    //bool moving = false;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        IsGrounded(); //need for landing animation

        rigidBody.velocity = new Vector2(horizontalInput * movementSpeed, rigidBody.velocity.y);
        if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (jumpCount > 0)
        {
            if (IsGrounded())
            {
                jumpCount = 0;
            }
            else //falling
            {
                Debug.Log("falling");
                
            }
        }
    }

    private bool IsGrounded()
    {
        //landing animation
        bool isGrounded = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, LayerMask.GetMask("Obstacle"));
        anim.SetBool("Jumping_Up", false);
        anim.SetBool("Landing", isGrounded);
        
        return isGrounded;
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalInput = context.ReadValue<Vector2>().x;

        bool walking = Convert.ToBoolean(Math.Abs(horizontalInput)); //1 - right, -1 - left, 0 - idle
            

        //Debug.Log("movement = "+horizontalInput);
        anim.SetBool("Walking", walking);
        //anim.SetFloat("Movement", horizontalInput);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        if (jumpCount == multiJumpCount)
        {
            return;
        }
        Debug.Log("jump");
        anim.SetBool("Jumping_Up", true);
        anim.SetBool("Landing", false);
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0); //reset y velocity so the jump is from a "standing position"
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        jumpCount++;
    }
}
