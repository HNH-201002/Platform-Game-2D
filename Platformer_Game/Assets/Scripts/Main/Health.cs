using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private List<Image> sprites;
    [SerializeField] private float hurtCooldown = 2f;

    private int health;
    private bool stillStay;
    private float countdown;
    private Animator animator;

    private const string EnemyTag = "Enemy";

    private void Start()
    {
        animator = GetComponent<Animator>();
        health = sprites.Count; // Assuming the initial health is equal to the number of sprites
        countdown = hurtCooldown;
    }

    public void Hurt()
    {
        if (health <= 0)
        {
            return;
        }

        health -= 1;
        sprites[health].enabled = false;
        animator.SetTrigger("hurt");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(EnemyTag))
        {
            Hurt();
            stillStay = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(EnemyTag))
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0)
            {
                Hurt();
                countdown = hurtCooldown;
            }
        }
    }
}