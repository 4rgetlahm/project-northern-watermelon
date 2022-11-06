using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    MaggotMeleeEnemyAI movementController;

    void Start()
    {
        movementController.OnEChase += Chase;
        movementController.OnEIdle += Idle;
        movementController.OnEAttack += Attack;
        movementController.OnEEndAttack += EndAttack;
    }

    /*
    void Jump()
    {
        Debug.Log("Jump");
        animator.SetBool("Jumping_Up", true);
        animator.SetBool("Landing", false);
    }

    void Falling()
    {
        Debug.Log("Falling");
        animator.SetBool("Jumping_Up", false);
    }

    void Land()
    {
        Debug.Log("Land");
        animator.SetBool("Jumping_Up", false);
        animator.SetBool("Landing", true);
    }
    */

    void Chase()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Chase", true);
    }

    void Idle()
    {
        animator.SetBool("Chase", false);
        animator.SetBool("Idle", true);
    }

    void Attack()
    {
        animator.SetBool("Attack", true);
    }

    void EndAttack()
    {
        animator.SetBool("Attack", false);
    }

}
