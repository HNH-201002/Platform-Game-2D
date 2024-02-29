using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject hitCheckPoint; // The object through which the isGrounded check is performed.
    public float hitCheckRadius; // isGrounded check radius.
    public LayerMask hitLayer; // Layer wich the character can jump on.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "HitPoint")
        {
            if (collision.GetComponentInParent<StateController>() != null)
            {
                collision.GetComponentInParent<StateController>().Hurt();
            }
            else
            {
                collision.GetComponentInParent<Plant>().Hurt();
            }
            collision.GetComponentInParent<HealthEnemy>().Hurt();
            GetComponentInParent<Movement>().Attack();
        }
    }
}
