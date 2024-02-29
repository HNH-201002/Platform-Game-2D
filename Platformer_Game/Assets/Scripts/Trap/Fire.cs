using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    Animator animator;
    [SerializeField] private GameObject fireObject;
    private void Start()
    {
        animator = GetComponent<Animator>();
        if(fireObject != null)
        {
            fireObject.SetActive(false);
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            animator.SetBool("Hit",true);
            fireObject.SetActive(true);
        }
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            animator.SetBool("Hit", false);
            fireObject.SetActive(false);
        }
    }
}
