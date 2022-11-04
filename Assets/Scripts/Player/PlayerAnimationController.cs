using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    MovementController movementController;

    void Start()
    {
        movementController.OnJump += Jump;
        movementController.OnLand += Land;
        movementController.OnMove += Move;
        movementController.OnStop += Stop;
    }

    void Jump()
    {
        Debug.Log("Jump");
        animator.SetBool("Jumping_Up", true);
        animator.SetBool("Landing", false);
    }

    void Land()
    {
        Debug.Log("Land");
        animator.SetBool("Jumping_Up", false);
        animator.SetBool("Landing", true);
    }

    void Move()
    {
        animator.SetBool("Walking", true);
    }

    void Stop()
    {
        animator.SetBool("Walking", false);
    }
}
