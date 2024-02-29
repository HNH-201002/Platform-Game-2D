using UnityEngine;


public class ChaseState : MonoBehaviour, IState 
{
    EnemyBase enemy;
    public void OnEnter(StateController state)
    {
         enemy = state.GetComponent<EnemyBase>();
         enemy.Set(state);
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
        Collider2D hitPlayer = Physics2D.OverlapCircle(state.transform.position, state.radius, state.playerLayer);
        if (hitPlayer)
        {
            if (Mathf.Abs(hitPlayer.transform.position.x - state.transform.position.x) <= 1.9f) 
            {
                state.animator.SetFloat("speed", 0);
                return;
            }
            Vector2 direction = (hitPlayer.transform.position - state.transform.position).normalized;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            enemy.ChaseTarget(targetAngle);
            state.animator.SetFloat("speed", state.GetChaseSpeed());
            if (direction.x > 0)
            {
                state.transform.eulerAngles = new Vector3(state.transform.eulerAngles.x, 180, state.transform.eulerAngles.z);
            }
            else if (direction.x < 0)
            {
                state.transform.eulerAngles = new Vector3(state.transform.eulerAngles.x, 0, state.transform.eulerAngles.z);
            }
        }
        else
        {
            OnExit(state, state.patrolState);
        }
    }
}
