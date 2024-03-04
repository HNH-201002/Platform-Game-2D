using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private List<Image> sprites;
    [SerializeField] private float hurtCooldown = 1f;
    public static event Action OnPlayerDied;

    private int health;
    private Animator animator;

    private const string EnemyTag = "Enemy";
    private const string SOUND_HURT_NAME = "Hurt";
    private const string SOUND_DIED_NAME = "Died";

    private readonly int hurtHash = Animator.StringToHash("hurt"); 
    private float countdown;

    private void Start()
    {
        animator = GetComponent<Animator>();
        health = sprites.Count; 
    }
    public void Update()
    {
        if (countdown >= 0) countdown -= Time.deltaTime;
    }

    public void Hurt()
    {
        if (health < 0) return;
        if (countdown >= 0) return;
        health -= 1;
        if (health >= 0 && health < sprites.Count)
        {
            sprites[health].enabled = false;
        }
        animator.SetTrigger(hurtHash);
        countdown = hurtCooldown;
        SoundManager.Instance.PlaySound(SOUND_HURT_NAME);

        if (health == 0)
        {
            Die();
            return;
        }
    }
    private void Die() => StartCoroutine(DieWithDelay());

    private IEnumerator DieWithDelay()
    {
        SoundManager.Instance.PlaySound(SOUND_DIED_NAME);
        OnPlayerDied?.Invoke();
        Time.timeScale = 0.3f;
        yield return new WaitForSecondsRealtime(1.5f);
        Time.timeScale = 0;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(EnemyTag))
        {
            Hurt();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(EnemyTag))
        {
            Hurt();
        }
    }
}