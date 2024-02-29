using UnityEngine;

public class NoFly : EnemyBase
{
    RaycastHit2D raycastHit;
    int rand;
    private float rotate;
    public override void ChaseTarget(float targetAngle)
    {
        state.body.velocity = new Vector2(Mathf.Cos(targetAngle * Mathf.Deg2Rad) * state.GetChaseSpeed(), state.body.velocity.y);
    }

    public override void Patrol(bool isFacing,float originalPosY)
    {
        rotate = isFacing ? 0 : 180;
        state.transform.eulerAngles = new Vector3(state.transform.eulerAngles.x, rotate, state.transform.eulerAngles.z);
        float speed = isFacing == true ? -state.GetSpeed() : state.GetSpeed();
        state.body.velocity = new Vector2(speed, state.body.velocity.y);
        state.animator.SetFloat("speed", Mathf.Clamp(state.GetSpeed(), 0, 1));
    }

    public override void RunAway()
    {
        rand = Random.Range(0, 1);
        if (rand == 0)
        {
            raycastHit = Physics2D.Raycast(state.rightPoint.transform.position + new Vector3(0, -0.3f, 0), Vector2.left, 0.3f, state.obstacleLayer);
            if (raycastHit.collider)
            {
                GetComponent<ChaseState>().OnExit(state, state.patrolState);
                return; // Stop further execution if we're exiting the state
            }
            state.body.position = Vector3.MoveTowards(state.body.position, new Vector2(state.body.position.x - 4, state.body.position.y), 6 * Time.deltaTime);
            state.transform.eulerAngles = new Vector3(state.transform.eulerAngles.x, 0, state.transform.eulerAngles.z);
        }
        else
        {


            raycastHit = Physics2D.Raycast(state.rightPoint.transform.position + new Vector3(0, -0.3f, 0), Vector2.right, 0.3f, state.groundLayer);
            if (raycastHit.collider)
            {
                GetComponent<ChaseState>().OnExit(state, state.patrolState);
                return; // Stop further execution if we're exiting the state
            }
            state.transform.eulerAngles = new Vector3(state.transform.eulerAngles.x, 180, state.transform.eulerAngles.z);
            state.body.position = Vector3.MoveTowards(state.body.position, new Vector2(state.body.position.x + 4, state.body.position.y), 6 * Time.deltaTime);
        }

        state.animator.SetFloat("speed", 1);
    }
}
