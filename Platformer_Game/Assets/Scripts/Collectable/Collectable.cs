using System.Collections;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public float delayBeforeDestroy = 0.4f;
    Collider2D collectableCollider;
    Animator ani;
    public void Start()
    {
        ani = GetComponent<Animator>(); 
        collectableCollider = GetComponent<Collider2D>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.GetComponent<CollectableSystem>().Add(1);
            collectableCollider.enabled = false;
            StartCoroutine(DestroyAfterDelay());
        }
    }

    private IEnumerator DestroyAfterDelay()
    {
        ani.SetTrigger("Destroy");
        yield return new WaitForSeconds(delayBeforeDestroy);

        // Then destroy the object
        Destroy(gameObject);
    }
}