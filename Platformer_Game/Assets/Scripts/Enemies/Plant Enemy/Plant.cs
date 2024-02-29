using UnityEngine;
using System.Collections;

public class Plant : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Vector3 position;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private float shotDelay = 1.0f; // Delay between shots
    private Animator animator;
    private bool canShoot = true; // Whether the plant can shoot again
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
        canShoot = false; // Disable shooting

        GameObject bulletObject = Instantiate(bulletPrefab,shootPoint.position, Quaternion.identity);
        Vector2 direction = DirectionShoot.Right == directionShoot ? Vector2.right : Vector2.left;
        bulletObject.GetComponent<Bullet>().direction = direction; // Set the direction for the bullet

        animator.SetTrigger("Attack");
        Destroy(bulletObject, 2); // Adjust the time before destroying the bullet if needed

        yield return new WaitForSeconds(shotDelay); // Wait for the delay

        canShoot = true; // Enable shooting again
    }
    void OnDrawGizmos()
    {
        // Set the Gizmo color to a color of your choice, e.g., red with some transparency
        Gizmos.color = new Color(1, 0, 0, 0.5f);

        // Draw the OverlapCircle as a sphere Gizmo
        Gizmos.DrawWireSphere(transform.position + position, radius);
    }
    public void Hurt()
    {
        animator.SetTrigger("Hit");
    }
}