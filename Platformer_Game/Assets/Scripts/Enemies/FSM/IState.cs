using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void OnEnter(StateController state);
    public void UpdateState(StateController state);
    public void OnHurt();
    public void OnExit(StateController state);
}
