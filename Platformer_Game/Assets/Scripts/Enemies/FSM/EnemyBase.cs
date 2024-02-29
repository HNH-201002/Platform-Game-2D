using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    protected StateController state;
    protected Animator animator;

    // Common methods for all enemies
    public abstract void ChaseTarget(float targetAngle);

    public abstract void RunAway();
    public abstract void Patrol(bool isFacing, float originalPosY);
    public void Set(StateController state)
    {
        this.state = state;
    }
}
