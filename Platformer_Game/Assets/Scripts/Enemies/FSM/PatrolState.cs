using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PatrolState : IState
{
    private bool isFacing;
    Vector2 direction = Vector2.left;
    float rotate = 180;
    public void OnEnter(StateController state)
    {
        state.transform.eulerAngles = new Vector3(state.transform.eulerAngles.x, 180, state.transform.eulerAngles.z);
    }

    public void UpdateState(StateController state)
    {
        bool hitGround = Physics2D.Raycast(state.transform.position,direction,1.5f, state.groundLayer);
        bool hitPlayer = Physics2D.Raycast(state.transform.position, direction, 3f, state.playerLayer);
        Debug.DrawRay(state.transform.position,direction * 1.5f, Color.red);
        if (hitPlayer)
        {
            OnExit(state);
        }
        if (hitGround)
        {
            rotate = isFacing == true ? 180 : 0;
            Debug.Log(rotate);
            isFacing = !isFacing;
        }
        if (!isFacing)
        {
            state.body.velocity = new Vector2(state.speed, state.body.velocity.y);
        }
        else
        {
            state.body.velocity = new Vector2(-state.speed, state.body.velocity.y);
        }
        state.transform.eulerAngles = new Vector3(state.transform.eulerAngles.x, rotate, state.transform.eulerAngles.z);
        direction = isFacing ? Vector2.left : Vector2.right;
        state.animator.SetFloat("speed", Mathf.Clamp(state.speed, 0, 1));
    }
    public void OnExit(StateController state)
    {
        state.ChangeState(state.chaseState);
    }
    public void OnHurt()
    {
      
    }
}
