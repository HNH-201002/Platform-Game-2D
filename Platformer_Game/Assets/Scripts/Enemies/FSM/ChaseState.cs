using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ChaseState : IState
{
    private bool isFacing;
    Vector2 direction = Vector2.left;
    float rotate = 180;
    float angle;
    float speed;
    public void OnEnter(StateController state)
    {
        state.speed = 3;
        speed = state.speed;
    }
    public void UpdateState(StateController state)
    {
        ChaseTarget(state);
    }

    public void OnExit(StateController state,IState state1)
    {
        state.ChangeState(state1);
    }

    public void OnHurt(StateController state)
    {
        OnExit(state,state.runAwayState);
    }
    private void ChaseTarget(StateController state)
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(state.transform.position,state.radius, state.playerLayer);
        if (hitPlayer.Length > 0)
        {
            state.body.velocity = new Vector2(state.speed, state.body.velocity.y);
            state.transform.eulerAngles = new Vector3(state.transform.eulerAngles.x, 0, state.transform.eulerAngles.z);

            foreach (var target in hitPlayer)
            {
                Vector2 direction = target.transform.position - state.transform.position;
                direction.Normalize();

                // Tính góc giữa hướng hiện tại của kẻ thù và hướng đến người chơi
                angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                bool distance = Mathf.Abs(target.transform.position.x - state.transform.position.x) <= state.distanceToStopChase;
                state.speed = distance ? 0 : speed;
                state.animator.SetFloat("speed", Mathf.Clamp(state.speed, 0, 1));

            }
            if (angle > -90)
            {
                state.body.velocity = new Vector2(state.speed, state.body.velocity.y); // Move right physics.
                state.transform.eulerAngles = new Vector3(state.transform.eulerAngles.x, 180, state.transform.eulerAngles.z);
            }
            else
            {
                state.body.velocity = new Vector2(-state.speed, state.body.velocity.y);
                state.transform.eulerAngles = new Vector3(state.transform.eulerAngles.x, 0, state.transform.eulerAngles.z);
            }

        }
        else
        {
            OnExit(state,state.patrolState);
        }
    }
}
