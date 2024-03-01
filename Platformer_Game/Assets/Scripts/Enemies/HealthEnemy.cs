using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    [SerializeField] private GameObject healthPoint;
    [SerializeField] private GameObject healthGameObject;
    [SerializeField] private int healthCount = 5;

    private List<GameObject> gameObjects = new List<GameObject>();
    private float countdown;
    private const float HeartSpacing = 0.8f; // Spacing between hearts
    private const float OffsetForTwo = 0.5f; // Additional offset when there are two hearts
    private const float HurtCooldown = 0.2f; // Cooldown duration after getting hurt
    private const string SOUNDNAME = "Hurt_Enemy";
    public static Action OnEnemyDied;
    void Start()
    {
        Vector2 startPosition = healthPoint.transform.position;
        float offset = healthCount == 2 ? OffsetForTwo : HeartSpacing / 2;

        for (int i = 0; i < healthCount; i++)
        {
            Vector2 positionOffset = new Vector2(startPosition.x + (i - (healthCount / 2)) * HeartSpacing + offset, startPosition.y);
            GameObject heartObj = Instantiate(healthGameObject, positionOffset, Quaternion.identity, healthPoint.transform);
            gameObjects.Add(heartObj);
        }
    }

    void Update()
    {
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
        }
    }

    public void Hurt()
    {
        if (countdown > 0) return;

        countdown = HurtCooldown;
        if (healthCount > 0)
        {
            healthCount--;
            gameObjects[healthCount].SetActive(false);
            SoundManager.Instance.PlaySound(SOUNDNAME);
        }

        if (healthCount <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnEnemyDied?.Invoke();
        StartCoroutine(DelayBeforeDestroyObject());
    }
    private IEnumerator DelayBeforeDestroyObject()
    {
        if(GetComponent<StateController>() != null) { GetComponent<StateController>().enabled = false; }
        GetComponent<Collider2D>().enabled = false;
        GetComponentInChildren<Collider2D>().enabled = false;
        GetComponent<Animator>().SetBool("Die",true);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Add a small upward force to make the object jump
            float jumpForce = 200f; // Adjust the force as needed
            rb.AddForce(new Vector2(0, jumpForce));
            rb.constraints = RigidbodyConstraints2D.None;
            float rotationAmount = 20f; 
            rb.angularVelocity = rotationAmount;
        }
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }
}