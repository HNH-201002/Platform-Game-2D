using System.Collections.Generic;
using UnityEngine;

public class ReSpawn : MonoBehaviour
{
    [SerializeField] private float limitedPosY;
    [SerializeField] private List<Transform> respawnPos;
    private Health health;
    private void Start()
    {
        health = GetComponent<Health>();
    }
    private void Update()
    {
        if (transform.position.y < limitedPosY)
        {
            Transform closestRespawnPoint = GetClosestRespawnPoint();
            RespawnAtPoint(closestRespawnPoint);
            health.Hurt();
        }
    }

    private Transform GetClosestRespawnPoint()
    {
        Transform closestPoint = null;
        float minDistance = Mathf.Infinity;

        foreach (Transform point in respawnPos)
        {
            float distance = Mathf.Abs(point.position.x - transform.position.x);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestPoint = point;
            }
        }

        return closestPoint;
    }

    private void RespawnAtPoint(Transform respawnPoint)
    {
        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
        }
    }
}