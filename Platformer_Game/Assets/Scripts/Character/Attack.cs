using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject hitCheckPoint;
    public LayerMask hitLayers; 
    public float castWidth;
    public float castHeight;

    private void Update()
    {
        CheckForHits();
    }

    private void CheckForHits()
    {
        Vector2 point = new Vector2(hitCheckPoint.transform.position.x, hitCheckPoint.transform.position.y);
        Vector2 size = new Vector2(castWidth, castHeight);
        float angle = 0f; 
        Vector2 direction = Vector2.right; 
        float distance = 0f; 

        RaycastHit2D hit = Physics2D.BoxCast(point, size, angle, direction, distance, hitLayers);
        if (hit.collider != null)
        {
            if (hit.collider.GetComponentInParent<StateController>() != null)
            {
                hit.collider.GetComponentInParent<StateController>().Hurt();
            }
            else if (hit.collider.GetComponentInParent<Plant>() != null)
            {
                hit.collider.GetComponentInParent<Plant>().Hurt();
            }
            hit.collider.GetComponentInParent<HealthEnemy>().Hurt();
            GetComponentInParent<Movement>().Attack();
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (hitCheckPoint != null)
        {
            Gizmos.color = Color.red;
            var point = new Vector2(hitCheckPoint.transform.position.x, hitCheckPoint.transform.position.y);
            var size = new Vector2(castWidth, castHeight);
            var matrix = Matrix4x4.TRS(hitCheckPoint.transform.position, hitCheckPoint.transform.rotation, hitCheckPoint.transform.lossyScale);
            Gizmos.matrix = matrix;
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(size.x, size.y, 0.01f));
        }
    }
}