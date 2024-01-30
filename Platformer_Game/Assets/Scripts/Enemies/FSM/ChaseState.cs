using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ChaseState : IState
{
    private bool isFacing;
    Vector2 direction = Vector2.left;
    float rotate = 180;
    public void OnEnter(StateController state)
    {
        state.speed = 3;
    }
    public void UpdateState(StateController state)
    {
        RaycastHit2D hitPlayer = Physics2D.Raycast(state.transform.position, direction, 5f, state.playerLayer);
        if (hitPlayer)
        {
            state.body.velocity = new Vector2(state.speed, state.body.velocity.y);
            state.transform.eulerAngles = new Vector3(state.transform.eulerAngles.x, 0,state.transform.eulerAngles.z);


            Vector2 direction =hitPlayer.collider.transform.position - state.transform.position;
            direction.Normalize();

            // Tính góc giữa hướng hiện tại của kẻ thù và hướng đến người chơi
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (angle > -90)
            {
                state.body.velocity = new Vector2(state.speed, state.body.velocity.y); // Move right physics.
                state.transform.eulerAngles = new Vector3(state.transform.eulerAngles.x,180, state.transform.eulerAngles.z);
            }
            else
            {
                state.body.velocity = new Vector2(-state.speed, state.body.velocity.y);
                state.transform.eulerAngles = new Vector3(state.transform.eulerAngles.x, 0, state.transform.eulerAngles.z);
            }

        }
        else
        {
            OnExit(state);
        }
    }
    public void OnExit(StateController state)
    {
        state.ChangeState(state.patrolState);
    }

    public void OnHurt()
    {
        throw new System.NotImplementedException();
    }

}
