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
    private Seeker seeker;
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    public System.Action OnDestinationReached;

    private Transform _target = null;
    public Transform Target
    {
        get
        {
            return _target;
        }
        set
        {
            _target = value;
            if (_target != null)
            {
                GeneratePath();
            }
        }
    }

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        InvokeRepeating("GeneratePath", 0f, 0.5f);
    }

    public void ChangeSpeed(float newspeed)
    {
        speed = newspeed;
    }

    public float GetSpeed()
    {
        return speed;
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, LayerMask.GetMask("Obstacle"));
    }

    void GeneratePath()
    {
        if (Target == null)
        {
            return;
        }
        if (seeker.IsDone())
        {
            seeker.StartPath(transform.position, new Vector3(Target.position.x, Target.position.y, 0), OnPathGenerationComplete);
        }
    }

    void OnPathGenerationComplete(Path p)
    {
        if (p.error)
        {
            return;
        }

        currentPath = p;
        currentWaypoint = 0;
    }

    void FixedUpdate()
    {
        if (currentPath == null)
        {
            return;
        }
        if (currentWaypoint >= currentPath.vectorPath.Count)
        {
            return;
        }

        float distanceToTarget = Vector2.Distance(transform.position, Target.position);
        Debug.Log(Target.position);
        if (distanceToTarget < 1f)
        {
            rigidBody.velocity = Vector2.zero;
            OnDestinationReached?.Invoke();
            return;
        }

        Vector2 direction = ((Vector2)currentPath.vectorPath[currentWaypoint] - (Vector2)transform.position).normalized;
        Vector2 force = direction * speed;
        rigidBody.velocity = new Vector2(force.x, rigidBody.velocity.y);

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
