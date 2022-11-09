using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PathfinderMovement : MonoBehaviour
{
    private Path currentPath;
    private int currentWaypoint = 0;

    [SerializeField]
    private float speed = 200f;
    [SerializeField]
    private bool canJump = false;
    [SerializeField]
    private float jumpForce = 400f;
    private float nextWaypointDistance = 3f;
    private Seeker seeker;
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;

    private Transform _target = null;
    public Transform Target
    {
        get
        {
            return _target;
        }
        set
        {
            if (_target != value)
            {
                ResetPath();
            }
            _target = value;
            UpdatePathToTarget();
        }
    }

    public Vector2? targetPosition;

    public void SetDestination(Vector2 position)
    {
        ResetPath();
        targetPosition = position;
    }

    public Vector2 GetDestination()
    {
        if (targetPosition.HasValue)
        {
            return targetPosition.Value;
        }
        else if (Target != null)
        {
            return Target.position;
        }
        else
        {
            return transform.position;
        }
    }

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        InvokeRepeating("UpdatePathToTarget", 0f, 0.5f);
        InvokeRepeating("GeneratePath", 0f, 0.5f);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, LayerMask.GetMask("Obstacle"));
    }

    public void ResetPath()
    {
        targetPosition = null;
        currentPath = null;
        currentWaypoint = 0;
    }

    void UpdatePathToTarget()
    {
        if (Target == null)
        {
            return;
        }
        targetPosition = Target.position;
    }

    void GeneratePath()
    {
        if (targetPosition == null)
        {
            return;
        }
        if (seeker.IsDone())
        {
            seeker.StartPath(rigidBody.position, new Vector3(targetPosition.Value.x, targetPosition.Value.y, 0), OnPathGenerationComplete);
        }
    }

    void OnPathGenerationComplete(Path p)
    {
        Debug.Log("Generated path");
        if (p.error)
        {
            return;
        }
        if (targetPosition == null)
        {
            return;
        }

        currentPath = p;
        currentWaypoint = 0;
    }

    void FixedUpdate()
    {
        if (currentPath == null || targetPosition == null)
        {
            return;
        }
        if (currentWaypoint >= currentPath.vectorPath.Count)
        {
            return;
        }

        float distanceToTarget = Vector2.Distance(transform.position, targetPosition.Value);
        if (distanceToTarget < 1f)
        {
            ResetPath();
            rigidBody.velocity = Vector2.zero;
            return;
        }

        Vector2 direction = ((Vector2)currentPath.vectorPath[currentWaypoint] - (Vector2)transform.position).normalized;
        Vector2 force = direction * speed;
        rigidBody.velocity = new Vector2(force.x, rigidBody.velocity.y);

        rigidBody.AddForce(force);

        if (force.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (force.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (canJump && direction.y > 0.5f && IsGrounded())
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        }

        float distance = Vector2.Distance((Vector2)transform.position, currentPath.vectorPath[currentWaypoint]);

        if (distance < 1f)
        {
            currentWaypoint++;
        }
    }
}
