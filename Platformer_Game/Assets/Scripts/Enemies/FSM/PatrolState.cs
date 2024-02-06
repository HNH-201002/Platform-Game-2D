using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PatrolState : MonoBehaviour,IState
{
    private bool isFacing;
    Vector2 direction = Vector2.left;
    float rotate = 180;
    float countdown = 0;
    public void OnEnter(StateController state)
    {
        state.transform.eulerAngles = new Vector3(state.transform.eulerAngles.x, 180, state.transform.eulerAngles.z);
    }

    public void UpdateState(StateController state)
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(state.transform.position, state.radius, state.playerLayer);
        if (hitPlayer.Length > 0)
        {
            OnExit(state,state.chaseState);
        }
        if (countdown >= 0)
        {
            countdown -= Time.deltaTime;
        }
        if (state.typePatrol == StateController.TypePatrol.RotateDirectionAwayGround)
        {
            bool hitGround = Physics2D.Raycast(state.transform.position, direction, 1.5f, state.groundLayer);
            Debug.DrawRay(state.transform.position, direction * 1.5f, Color.red);
            RotateDirectionAwayGround(state, hitGround);
        }
        if (state.typePatrol == StateController.TypePatrol.RotateByDistance)
        {
            RotateByDistance(state);
        }
    }
    void RotateDirectionAwayGround(StateController state, bool hitGround)
    {
        if (hitGround)
        {
            rotate = isFacing == true ? 180 : 0;
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
    private void RotateByDistance(StateController state)
    {
        if (countdown > 0)
        {
            return;
        }
        if (Mathf.Abs(state.transform.position.x - state.originalPosition.x) >= state.distancePatrol)
        {
            rotate = isFacing == true ? 180 : 0;
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
        countdown = 0.5f;
    }

    public void OnExit(StateController state, IState state1)
    {
        state.ChangeState(state1);
    }

    public void OnHurt(StateController state)
    {
        
    }
}
