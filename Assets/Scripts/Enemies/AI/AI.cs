using UnityEngine;

public class AIStateArgs
{
    public dynamic state;

    public AIStateArgs(dynamic state)
    {
        this.state = state;
    }
}

public class AI : MonoBehaviour
{
    private Transform playerTransform;
    private EnemyHealth enemyHealth;
    private PathfinderMovement pathfinderMovement;

    private PlayerController playerController;

    private Vector2 spawnPosition;

    protected dynamic state;

    public event System.Action<AIStateArgs> OnAIStateChange;
    public delegate void AIStateChange(AIStateArgs AIStateArgs);


    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = playerTransform.GetComponent<PlayerController>();
        spawnPosition = transform.position;
    }
    protected void SetState(dynamic state)
    {
        this.state = state;
        Debug.Log("ffaaa  " + state);
        OnAIStateChange?.Invoke(new AIStateArgs(state));
    }

}
