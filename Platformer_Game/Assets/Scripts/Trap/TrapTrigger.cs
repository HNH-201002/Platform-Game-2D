using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    private float countdown = 0.5f;

    void Update()
    {
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (countdown > 0) return;
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Health>().Hurt();
            countdown = 0.5f;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (countdown > 0) return;
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Health>().Hurt();
            countdown = 0.5f;
        }
    }
}