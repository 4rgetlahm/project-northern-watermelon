using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum EnemyState
{
    Idle,
    GoingToSpawn,
    Patrol,
    Chase,
    Attack
}

public class MeleeEnemyAI : AI
{
    [SerializeField]
    private float triggerRadius = 5f;
    [SerializeField]
    private float attackRadius = 1f;
    [SerializeField]
    private float delayBetweenAttacks = 1f;
    private float lastAttackTime = 0f;

    [SerializeField]
    private Transform attackPoint = null;

    private Transform playerTransform;
    private EnemyHealth enemyHealth;
    private PathfinderMovement pathfinderMovement;

    private PlayerController playerController;

    private Vector2 spawnPosition;

    void Start()
    {
        state = EnemyState.Idle;
        enemyHealth = GetComponent<EnemyHealth>();
        pathfinderMovement = GetComponent<PathfinderMovement>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = playerTransform.GetComponent<PlayerController>();
        spawnPosition = transform.position;
        if (attackPoint == null)
        {
            attackPoint = transform;
        }
    }
    void FixedUpdate()
    {
        Type thisType = this.GetType();
        MethodInfo method = thisType.GetMethod(state.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);
        method.Invoke(this, null);
    }

    protected virtual void Idle()
    {
        if (Vector2.Distance(spawnPosition, playerTransform.position) < triggerRadius)
        {
            SetState(EnemyState.Chase);
        }
    }

    protected virtual void GoingToSpawn()
    {
        if (pathfinderMovement.GetDestination() != spawnPosition)
        {
            pathfinderMovement.SetDestination(spawnPosition);
        }
        if (Vector2.Distance(spawnPosition, transform.position) < 100000)
        {
            SetState(EnemyState.Idle);
            return;
        }
    }

    protected virtual void Patrol()
    {
        //TODO
    }

    protected virtual void Chase()
    {
        if (Vector2.Distance(spawnPosition, playerTransform.position) > triggerRadius)
        {
            pathfinderMovement.Target = null;
            SetState(EnemyState.GoingToSpawn);
            return;
        }
        else if (Vector2.Distance(attackPoint.position, playerTransform.position) <= attackRadius)
        {
            pathfinderMovement.Target = null;
            SetState(EnemyState.Attack);
            return;
        }
        if (pathfinderMovement.Target != playerTransform)
        {
            pathfinderMovement.Target = playerTransform;
        }
    }

    protected virtual void Attack()
    {
        if (lastAttackTime + delayBetweenAttacks < Time.time)
        {
            lastAttackTime = Time.time;
            StartCoroutine(ExecuteAttack());
        }
        if (Vector2.Distance(playerTransform.position, attackPoint.position) >= attackRadius)
        {
            SetState(EnemyState.Chase);
        }
    }


    protected virtual IEnumerator ExecuteAttack()
    {
        yield return new WaitForSeconds(1f);
        if (Vector2.Distance(playerTransform.position, attackPoint.position) <= attackRadius)
        {
            playerController.Hit();
        }
    }
}
