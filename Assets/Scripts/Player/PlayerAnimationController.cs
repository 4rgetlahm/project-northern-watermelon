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
        movementController.OnBackwardsMove += BackwardsMove;
        movementController.OnStop += Stop;
        movementController.OnFall += Falling;
    }

    void Jump()
    {
        animator.SetBool("Jumping_Up", true);
        animator.SetBool("Landing", false);
    }

    void Falling()
    {
        animator.SetBool("Jumping_Up", false);
    }

    void Land()
    {
        animator.SetBool("Jumping_Up", false);
        animator.SetBool("Landing", true);
    }

    void Move()
    {
        animator.SetBool("Walking", true);
        animator.SetBool("BackwardsWalking", false);
    }

    void BackwardsMove()
    {
        animator.SetBool("BackwardsWalking", true);
        animator.SetBool("Walking", false);
    }

    void Stop()
    {
        animator.SetBool("Walking", false);
        animator.SetBool("BackwardsWalking", false);
    }
}
