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

    void Start()
    {
        Vector2 startPosition = healthPoint.transform.position;
        float offset = healthCount == 2 ? OffsetForTwo : HeartSpacing / 2;

        for (int i = 0; i < healthCount; i++)
        {
            Vector2 positionOffset = new Vector2(startPosition.x + (i - (healthCount / 2)) * HeartSpacing + offset, startPosition.y);
            GameObject heartObj = Instantiate(healthGameObject, positionOffset, Quaternion.identity, healthPoint.transform);
            // Uncomment the line below if you want to deactivate the health objects initially
            // heartObj.SetActive(false);
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
        }

        if (healthCount <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}