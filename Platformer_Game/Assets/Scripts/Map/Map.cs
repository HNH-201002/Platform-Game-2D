using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private int mapNumber;
    [SerializeField] private MapManager mapManager;
    public void Start()
    {
        mapManager = FindObjectOfType<MapManager>().GetComponent<MapManager>();
    }

    public void GetMapNumber()
    {
        mapManager.SelectMap(mapNumber);
    }
}
