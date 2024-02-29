using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction;
    public float speed = 5.0f;
  
    void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().Hurt();
            Destroy(gameObject);
        }
    }
}
