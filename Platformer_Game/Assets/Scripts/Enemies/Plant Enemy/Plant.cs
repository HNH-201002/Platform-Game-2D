using UnityEngine;
using System.Collections;

public class Plant : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Vector3 position;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private float shotDelay = 1.0f;
    private Animator animator;
    private bool canShoot = true;

    private readonly int _hitAniHash = Animator.StringToHash("Hit");
    private readonly int _attackAniHash = Animator.StringToHash("Attack");

    enum DirectionShoot
    {
        Left,
        Right
    }
    [SerializeField] private DirectionShoot directionShoot;
    void Start()
    {
        animator = GetComponent<Animator>();
        if (DirectionShoot.Right == directionShoot)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    void Update()
    {
        if (canShoot)
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position + position, radius, playerLayerMask);
            if (collider)
            {
                StartCoroutine(Shoot(collider.transform.position));
            }
        }
    }

    IEnumerator Shoot(Vector2 targetPosition)
    {
        canShoot = false;

        GameObject bulletObject = Instantiate(bulletPrefab,shootPoint.position, Quaternion.identity);
        Vector2 direction = DirectionShoot.Right == directionShoot ? Vector2.right : Vector2.left;
        bulletObject.GetComponent<Bullet>().direction = direction;

        animator.SetTrigger(_attackAniHash);
        Destroy(bulletObject, 2);

        yield return new WaitForSeconds(shotDelay);

        canShoot = true;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireSphere(transform.position + position, radius);
    }
    public void Hurt() => animator.SetTrigger(_hitAniHash);
}