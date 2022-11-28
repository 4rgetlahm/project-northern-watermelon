using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class MaggotAnimationController : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    AI ai;

    void Start()
    {
        ai.OnAIStateChange += OnStateChange;
    }

    void OnStateChange(AIStateArgs args)
    {
        Type thisType = this.GetType();
        MethodInfo method = thisType.GetMethod(args.state.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);
        method.Invoke(this, null);
        Debug.Log(args.state);
    }

    void Chase()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Chase", true);
    }

    void Idle()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("Chase", false);
        animator.SetBool("Idle", true);
    }

    void GoingToSpawn()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("Chase", false);
        animator.SetBool("Idle", true);
    }

    void Attack()
    {
        animator.SetBool("Chase", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Attack", true);
    }

}
