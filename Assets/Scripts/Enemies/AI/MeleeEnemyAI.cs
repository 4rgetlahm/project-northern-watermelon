using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum EnemyState
{
    Idle,
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
        pathfinderMovement.OnDestinationReached += OnPathfinderDestinationReached;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = playerTransform.GetComponent<PlayerController>();
        spawnPosition = transform.position;
    }
    void FixedUpdate()
    {
        Vector2 targetDirection = ((Vector2)playerTransform.position - (Vector2)transform.position).normalized;
        if (targetDirection.x > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (targetDirection.x < 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

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

    protected virtual void Chase()
    {
        if (pathfinderMovement.Target != playerTransform)
        {
            pathfinderMovement.Target = playerTransform;
        }
    }

    protected virtual void Attack()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) >= attackRadius)
        {
            SetState(EnemyState.Chase);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Vector2 direction = (collision.transform.position - transform.position).normalized;
            playerController.GetComponent<MovementController>().Knockback(direction, 20f);
            if (Time.time - lastAttackTime > delayBetweenAttacks)
            {
                lastAttackTime = Time.time;
                playerController.Hit(1);
            }
        }
    }

    private void OnPathfinderDestinationReached()
    {
        if (state == EnemyState.Chase)
        {
            SetState(EnemyState.Attack);
        }
    }
}
