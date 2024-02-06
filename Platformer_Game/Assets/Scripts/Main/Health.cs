using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private List<Image> sprites;
    private int health;
    bool stillStay;
    float countdown = 2;
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        health = 3;
    }
    public void Hurt()
    {
        health -= 1;
        if (health < 0)
        {
            return;
        }
        sprites[health].enabled = false;
        animator.SetTrigger("hurt");
 
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Hurt();
            stillStay = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0)
            {
                Hurt();
                countdown = 2;
            }
        }
    }
}
