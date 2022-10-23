using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle,
    GoingToSpawn,
    Chase,
    Attack
}

public class MeleeEnemyController : MonoBehaviour
{
    [SerializeField]
    private float triggerRadius = 5f;
    [SerializeField]
    private float attackRadius = 1f;
    [SerializeField]
    private float delayBetweenAttacks = 1f;
    private float lastAttackTime = 0f;
    private EnemyState _state = EnemyState.Idle;
    private EnemyState State
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;
            ChangeAnimation(value.ToString());
        }
    }

    private Transform playerTransform;
    private EnemyHealth enemyHealth;
    private PathfinderMovement pathfinderMovement;
    private PlayerController playerController;
    private Vector2 spawnPosition;

    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        pathfinderMovement = GetComponent<PathfinderMovement>();
        enemyHealth.OnDamage += OnDamageTaken;
        enemyHealth.OnDeath += OnDeath;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = playerTransform.GetComponent<PlayerController>();
        spawnPosition = transform.position;
    }

    void FixedUpdate()
    {

        switch (State)
        {
            case EnemyState.Idle:
                if (Vector2.Distance(spawnPosition, playerTransform.position) < triggerRadius)
                {
                    State = EnemyState.Chase;
                }
                break;
            case EnemyState.GoingToSpawn:
                if (Vector2.Distance(spawnPosition, transform.position) < 1f)
                {
                    State = EnemyState.Idle;
                }
                break;
            case EnemyState.Chase:
                Debug.Log("Distance to player: " + Vector2.Distance(playerTransform.position, transform.position));
                if (Vector2.Distance(spawnPosition, playerTransform.position) > triggerRadius)
                {
                    State = EnemyState.GoingToSpawn;
                    pathfinderMovement.Target = null;
                    pathfinderMovement.SetDestination(spawnPosition);
                    return;
                }
                else if (Vector2.Distance(transform.position, playerTransform.position) <= attackRadius)
                {
                    pathfinderMovement.Target = null;
                    State = EnemyState.Attack;
                    return;
                }
                if (pathfinderMovement.Target != playerTransform)
                {
                    pathfinderMovement.Target = playerTransform;
                }
                break;
            case EnemyState.Attack:
                if (lastAttackTime + delayBetweenAttacks < Time.time)
                {
                    lastAttackTime = Time.time;
                    StartCoroutine(Attack());
                }
                if (Vector2.Distance(playerTransform.position, transform.position) >= attackRadius)
                {
                    State = EnemyState.Chase;
                }
                break;
            default:
                break;
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.5f);
        if (Vector2.Distance(playerTransform.position, transform.position) <= attackRadius)
        {
            playerController.Hit();
        }
    }

    private void ChangeAnimation(string animationName)
    {
        Debug.Log("ChangeAnimation: " + animationName);
        GetComponent<Animator>().Play(animationName);
    }

    public void OnDamageTaken(object sender, DamageTakenArgs e)
    {
        Debug.Log("Enemy took " + e.damageTaken + " damage");
    }

    public void OnDeath()
    {
        Debug.Log("Enemy died");
    }
}
