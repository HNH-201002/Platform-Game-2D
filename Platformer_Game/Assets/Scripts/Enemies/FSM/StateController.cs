using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public enum TypePatrol
    {
        RotateDirectionAwayGround,
        RotateByDistance
    }
    IState currentState;

    public float speed = 2f;
    public ChaseState chaseState = new ChaseState();
    public PatrolState patrolState = new PatrolState();
    public RunAwayState runAwayState = new RunAwayState();

    public Rigidbody2D body; // Variable for the RigidBody2D component.
    public Animator animator; // Variable for the Animator component. [OPTIONAL]
    public LayerMask groundLayer;
    public LayerMask playerLayer;
    public TypePatrol typePatrol;
    public Vector2 originalPosition;
    public float distancePatrol;
    public float radius;
    public float distanceToStopChase;

    public GameObject leftPoint;
    public GameObject rightPoint;
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
        Debug.Log(currentState);
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
}
