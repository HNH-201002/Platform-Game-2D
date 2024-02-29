using UnityEngine;

public class StateController : MonoBehaviour
{
    public enum TypePatrol
    {
        RotateDirectionAwayGround,
        RotateByDistance
    }
    IState currentState;

    [SerializeField] private EnemySO data;
    private float speed;
    private float chaseSpeed;
    public ChaseState chaseState;
    public PatrolState patrolState;
    public RunAwayState runAwayState;

    public Rigidbody2D body; 
    public Animator animator;
    public LayerMask groundLayer;
    public LayerMask playerLayer;
    public LayerMask obstacleLayer;

    public TypePatrol typePatrol;
    public Vector2 originalPosition;
    public float distancePatrol;
    public float radius;
    public float distanceToStopChase;

    public GameObject leftPoint;
    public GameObject rightPoint;

    private void Awake()
    {
        chaseState = new ChaseState();
        patrolState = new PatrolState();
        runAwayState = new RunAwayState();
        distancePatrol = data.DistancePatrol;
        speed = data.NormalSpeed;
        chaseSpeed = data.ChaseSpeed;
        radius = data.Radius;
    }

    private void Start()
    {
        originalPosition = transform.position;
        ChangeState(patrolState);
    }
    void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(this);
        }
    }
    public void ChangeState(IState newState)
    {
        currentState = newState;
        currentState.OnEnter(this);
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,radius);
    }
    public void Hurt()
    {
        animator.SetTrigger("hit");
        ChangeState(runAwayState);
    }

    public float GetChaseSpeed()
    {
        return chaseSpeed;
    }
    public float GetSpeed()
    {
        return speed;
    }
}
