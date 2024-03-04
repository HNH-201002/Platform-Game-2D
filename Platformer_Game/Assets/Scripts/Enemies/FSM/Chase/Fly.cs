using UnityEngine;

public class Fly : EnemyBase
{
    private float timer = 1f;
    private float rotate;
    public override void ChaseTarget(float targetAngle)
    {
        state.body.velocity = new Vector2(Mathf.Cos(targetAngle * Mathf.Deg2Rad) * state.GetChaseSpeed(), Mathf.Sin(targetAngle * Mathf.Deg2Rad) * state.GetChaseSpeed());
    }

    public override void Patrol(bool isFacing, float originalPosY)
    {
        rotate = isFacing ? 0 : 180;
        state.transform.eulerAngles = new Vector3(state.transform.eulerAngles.x, rotate, state.transform.eulerAngles.z);
        float speed = isFacing ? -state.GetSpeed() : state.GetSpeed();
        state.body.velocity = new Vector2(speed, 0);
    }

    public override void RunAway()
    {
        if (timer > 0)
        {
            state.body.position = Vector3.MoveTowards(state.body.position, state.body.position + Vector2.right * 2, state.GetSpeed() * Time.deltaTime);
            timer -= Time.deltaTime;
        }
        else
        {
            state.ChangeState(state.patrolState);
            timer = 1f;
        }
    }
}
