using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCollider : MonoBehaviour
{
    private float countdown = 0.5f;
    public void Update()
    {
        if (countdown >= 0)
        {
            countdown -= Time.deltaTime;
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (countdown >= 0) { return; }
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<Health>().Hurt();
            countdown = 0.5f;
        }
    }
    public void OnCollisionStay2D(Collision2D collision)
    {
        if (countdown >= 0) { return; }
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<Health>().Hurt();
            countdown = 0.5f;
        }
    }
}
