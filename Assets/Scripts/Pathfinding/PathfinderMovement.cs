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
    private float nextWaypointDistance = 3f;
    private Seeker seeker;
    private Rigidbody2D rigidBody;

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
            UpdatePath();
        }
    }

    void ResetPath()
    {
        currentPath = null;
        currentWaypoint = 0;
    }

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rigidBody = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (Target == null)
        {
            return;
        }
        if (seeker.IsDone())
        {
            seeker.StartPath(rigidBody.position, Target.position, OnPathGenerationComplete);
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

        Vector2 direction = ((Vector2)currentPath.vectorPath[currentWaypoint] - rigidBody.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rigidBody.AddForce(force);

        float distance = Vector2.Distance(rigidBody.position, currentPath.vectorPath[currentWaypoint]);

        if (distance < 1f)
        {
            currentWaypoint++;
        }

        if (force.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (force.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
