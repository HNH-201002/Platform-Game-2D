using UnityEngine;

public class RunAwayState :MonoBehaviour, IState
{
    EnemyBase enemy;
    public void UpdateState(StateController state)
    {
        enemy.RunAway();
    }
    public void OnEnter(StateController state)
    {
        enemy = state.GetComponent<EnemyBase>();
        enemy.Set(state);
    }

    public void OnExit(StateController state, IState state1)
    {
        state.ChangeState(state1);
    }

    public void OnHurt(StateController state)
    {
      
    }
}
