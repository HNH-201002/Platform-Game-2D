using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateController : MonoBehaviour
{
    IState currentState;

    public float speed = 2f;
    public ChaseState chaseState = new ChaseState();
    public PatrolState patrolState = new PatrolState();
    public Rigidbody2D body; // Variable for the RigidBody2D component.
    public Animator animator; // Variable for the Animator component. [OPTIONAL]
    public LayerMask groundLayer;
    public LayerMask playerLayer;

    private void Start()
    {
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
}
