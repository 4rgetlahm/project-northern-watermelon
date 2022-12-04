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
    [SerializeField]
    private GameObject arm;

    [SerializeField]
    private Camera theCam;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rigidBody;
    private int jumpCount = 0;
    private float horizontalInput;
    private float verticalInput;

    private bool isInAir = false;
    private bool isMoving = false;
    private bool isKnockedback = false;

    public event System.Action OnJump;
    public event System.Action OnLand;
    public event System.Action OnMove;
    public event System.Action OnStop;
    public event System.Action OnBackwardsMove;
    public event System.Action OnFall;

    //audio
    private AudioSource src;
    public AudioClip jump;
    public AudioClip walk;

    public int side = 0; //0 - left, 1 - right

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        //audio
        src = GetComponent<AudioSource>();
        //theCam = Camera.main;
    }

    void FixedUpdate()
    {
        if (isKnockedback)
        {
            return;
        }
        rigidBody.velocity = new Vector2(horizontalInput * movementSpeed, rigidBody.velocity.y);


        if (!IsGrounded()) //for jump cancelation
        {
            OnFall?.Invoke();
        }
        //left
        if (horizontalInput < 0 && side == 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        //right
        else if (horizontalInput > 0 && side == 1)
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


        //arm rotation
        Vector3 mouse = Input.mousePosition;
        
        Vector3 screenPoint = theCam.WorldToScreenPoint(transform.localPosition);

        Vector2 offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);

        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        //transformation
        float editAngle = 0;

        //angle is left
        if(angle > 90)
        {
            side = 0;
            angle = angle - 180;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        //angle left
        else if(angle < -90)
        {
            side = 0;
            angle = angle + 180;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        //right
        else
        {
            side = 1;
            transform.localScale = new Vector3(1, 1, 1);
        }


        //let angle be only 10 or -10 degrees
        if (angle > 10)
            editAngle = 10;
        else if (angle < -10)
            editAngle = -10;
        else
            editAngle = angle;

        arm.transform.rotation = Quaternion.Euler(0f, 0f, editAngle);
        //arm rotation
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.2f, LayerMask.GetMask("Obstacle"));
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalInput = context.ReadValue<Vector2>().x;

        if (horizontalInput == 0)
        {
            isMoving = false;
            OnStop?.Invoke();
            return;
        }

        //if moving
        if (side == 0 && horizontalInput > 0)
            OnBackwardsMove?.Invoke();
        else if (side == 1 && horizontalInput < 0)
            OnBackwardsMove?.Invoke();
        else
            OnMove?.Invoke();

        //audio
        src.clip = walk;
        src.Play();
        
        isMoving = true;
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
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0); //reset y velocity so the jump is from a "standing position"
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        jumpCount++;
        
        //audio
        src.clip = jump;
        src.Play();
        OnJump?.Invoke();
    }

    public void Knockback(Vector2 direction, float force)
    {
        StartCoroutine(KnockbackTimer(0.2f));
        rigidBody.velocity = new Vector2(0, 0);
        rigidBody.AddForce(direction * force, ForceMode2D.Impulse);
    }

    private IEnumerator KnockbackTimer(float timeSeconds)
    {
        isKnockedback = true;
        yield return new WaitForSeconds(timeSeconds);
        isKnockedback = false;
    }
}
