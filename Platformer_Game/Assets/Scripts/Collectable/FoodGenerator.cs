using System.Collections;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    [SerializeField] private int width; 
    [SerializeField] private int height;
    [SerializeField] private GameObject foodPrefab;

    void Start()
    {
        GameManager.Instance.RegisterFoodGenerator(this);
        StartCoroutine(GenerateFoodGrid());
    }
     
    private IEnumerator GenerateFoodGrid()
    {
        int halfWidth = width / 2;
        int halfHeight = height / 2;

        for (int i = -halfHeight; i <= halfHeight; i++)
        {
            for (int j = -halfWidth; j <= halfWidth; j++)
            {
                if (width % 2 == 0 && j == halfWidth) continue;
                if (height % 2 == 0 && i == halfHeight) continue;

                Vector3 positionOffset = new Vector3(j, i, 0);
                GameObject gameObj = Instantiate(foodPrefab, transform.position + positionOffset, Quaternion.identity);
                gameObj.transform.SetParent(transform);
                yield return null;
            }
        }
    }

    public int GetAmountOfFood()
    {
        return width * height;
    }
}