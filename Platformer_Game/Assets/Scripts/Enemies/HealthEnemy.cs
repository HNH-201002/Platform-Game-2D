using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    [SerializeField] private GameObject healthPoint;
    [SerializeField] private GameObject healthGameObject;
    [SerializeField] private int healthCount = 0;
    // Start is called before the first frame update
    List<GameObject> gameObjects = new List<GameObject>();
    GameObject heartObj;
    void Start()
    {
        for (int i = -1; i < healthCount - 1; i++) 
        {
            if (healthCount == 1)
            {
                heartObj = Instantiate(healthGameObject,
                new Vector2(healthPoint.transform.position.x, healthPoint.transform.position.y),
               Quaternion.identity);
                heartObj.transform.parent = healthPoint.transform;
            }
            else if (healthCount == 2)
            {
                heartObj = Instantiate(healthGameObject,
                 new Vector2(healthPoint.transform.position.x + i * 0.8f + 0.5f, healthPoint.transform.position.y),
                Quaternion.identity);
                heartObj.transform.parent = healthPoint.transform;
            }
            else
            {
                heartObj = Instantiate(healthGameObject,
                new Vector2(healthPoint.transform.position.x + i * 0.8f, healthPoint.transform.position.y),
                Quaternion.identity);
                heartObj.transform.parent = healthPoint.transform;
            }
            //heartObj.SetActive(false);
            gameObjects.Add(heartObj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Hurt()
    {
        healthCount -= 1;
        Debug.Log("healthcount" + healthCount);
        Debug.Log("number of lists " + gameObjects);
        gameObjects[healthCount].SetActive(false);
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
