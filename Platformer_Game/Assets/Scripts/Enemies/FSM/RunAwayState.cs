using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RunAwayState : IState
{
    RaycastHit2D raycastHit;
    RaycastHit2D raycastHit2Right;
    bool findWay = false;
    private Vector2 originalPos;
    int rand;
    public void UpdateState(StateController state)
    {
        if (rand == 0)
        {
            raycastHit = Physics2D.Raycast(state.rightPoint.transform.position, Vector2.left,0.3f, state.groundLayer);
            if (raycastHit.collider)
            {
                OnExit(state, state.patrolState);
            }
            state.body.position = Vector3.MoveTowards(state.body.position, new Vector2(state.body.position.x - 4, state.body.position.y), 6 * Time.deltaTime);
            state.transform.eulerAngles = new Vector3(state.transform.eulerAngles.x, 0, state.transform.eulerAngles.z);
        }
        else
        {
            raycastHit = Physics2D.Raycast(state.rightPoint.transform.position, Vector2.right,0.3f, state.groundLayer);
            if (raycastHit.collider)
            {
                OnExit(state, state.patrolState);
            }
            state.transform.eulerAngles = new Vector3(state.transform.eulerAngles.x, 180, state.transform.eulerAngles.z);
            state.body.position = Vector3.MoveTowards(state.body.position, new Vector2(state.body.position.x + 4, state.body.position.y), 6 * Time.deltaTime);
        }
     
        state.animator.SetFloat("speed", 1);
    }
    public void OnEnter(StateController state)
    {
        rand = Random.Range(0, 2);
        originalPos = state.body.transform.position;
    }

    public void OnExit(StateController state, IState state1)
    {
        findWay = false;
        state.ChangeState(state1);
    }

    public void OnHurt(StateController state)
    {
      
    }
}
