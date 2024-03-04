using UnityEngine;


public class PatrolState : MonoBehaviour,IState
{
    EnemyBase enemyBase = null;
    float originalPosY;
    private bool isFacing;
    public Vector3 offset1;

    private bool isFlipping;
    private float lastFlipTime;
    private float flipCooldown = 0.5f;
    public Vector2 boxCastSize = new Vector2(1f, 1f); 
    public float boxCastDistance = 2f; 
    public float boxCastAngle = 0f;
    public void OnEnter(StateController state)
    {
        state.transform.eulerAngles = new Vector3(state.transform.eulerAngles.x, 180, state.transform.eulerAngles.z);
        enemyBase = state.GetComponent<EnemyBase>();
        enemyBase.Set(state);
       originalPosY = state.transform.position.y; 
    }

    public void UpdateState(StateController state)
    {
        Vector2 direction = isFacing ? Vector2.left : Vector2.right;
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(state.transform.position, state.radius, state.playerLayer);
        RaycastHit2D hit = Physics2D.BoxCast(
           state.transform.position + offset1, 
           boxCastSize,
           boxCastAngle,
           direction, 
           boxCastDistance, 
           state.obstacleLayer 
       );

        if (!isFlipping)
        {
            if (Mathf.Abs(state.transform.position.x - state.originalPosition.x) >= state.distancePatrol)
            {
                Flip();
            }
            else
            {
                if (hit)
                {
                    if (hit.collider.name != state.gameObject.name)
                    {
                        Flip();
                    }
                }
            }
        }
        else
        {
            if (Time.time - lastFlipTime > flipCooldown)
            {
                isFlipping = false;
            }
        }


        if (hitPlayer.Length > 0)
        {
            OnExit(state, state.chaseState);
        }
        else
        {
            enemyBase.Patrol(isFacing, originalPosY);
        }
    }
    void Flip()
    {
        isFacing = !isFacing;
        isFlipping = true;
        lastFlipTime = Time.time;
    }
    public void OnExit(StateController state, IState state1)
    {
        state.ChangeState(state1);
    }

    public void OnHurt(StateController state)
    {
        
    }
}
