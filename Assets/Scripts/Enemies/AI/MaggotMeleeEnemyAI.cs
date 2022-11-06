using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaggotMeleeEnemyAI : MonoBehaviour
{
    [SerializeField]
    private float triggerRadius = 5f;
    [SerializeField]
    private float attackRadius = 1f;
    [SerializeField]
    private float delayBetweenAttacks = 1f;
    private float lastAttackTime = 0f;
    [SerializeField]
    private EnemyState state = EnemyState.Idle;

    private Transform playerTransform;
    private EnemyHealth enemyHealth;
    private PathfinderMovement pathfinderMovement;

    private PlayerController playerController;

    private Vector2 spawnPosition;

    //animations
    public event System.Action OnEChase;
    public event System.Action OnEIdle;
    public event System.Action OnEAttack;
    public event System.Action OnEEndAttack;

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

    // Update is called once per frame
    void FixedUpdate()
    {

        switch (state)
        {
            case EnemyState.Idle:
                if (Vector2.Distance(spawnPosition, playerTransform.position) < triggerRadius)
                {
                    state = EnemyState.Chase;

                    //animation
                    OnEChase?.Invoke();
                }
                break;
            case EnemyState.GoingToSpawn:
                if (Vector2.Distance(spawnPosition, transform.position) < 1f)
                {
                    state = EnemyState.Idle;

                    //animation
                    OnEIdle?.Invoke();
                }
                break;
            case EnemyState.Patrol:
                break;
            case EnemyState.Chase:
                if (Vector2.Distance(spawnPosition, playerTransform.position) > triggerRadius)
                {
                    state = EnemyState.GoingToSpawn;
                    pathfinderMovement.Target = null;
                    pathfinderMovement.SetDestination(spawnPosition);
                    return;
                }
                else if (Vector2.Distance(transform.position, playerTransform.position) <= attackRadius)
                {
                    pathfinderMovement.Target = null;
                    state = EnemyState.Attack;

                    //animation
                    OnEAttack?.Invoke();
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
                    state = EnemyState.Chase;

                    //animation
                    OnEEndAttack?.Invoke();
                    OnEChase?.Invoke();
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

    public void OnDamageTaken(object sender, DamageTakenArgs e)
    {
        Debug.Log("Enemy took " + e.damageTaken + " damage");
    }

    public void OnDeath()
    {
        Debug.Log("Enemy died");
    }
}
