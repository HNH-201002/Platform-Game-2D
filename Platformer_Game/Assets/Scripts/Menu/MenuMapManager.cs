using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMapManager : MonoBehaviour
{
    [SerializeField] private GameObject mapPrefab;
    [SerializeField] private GameObject containMap;
    private List<GameObject> maps = new List<GameObject>();
    private const int SCREEN_SYSTEM = 3;
    int mapNumberIsUnLocked;
    void Start()
    {
        LoadMap();
    }

    private void LoadMap()
    {
        GameData gameData = SaveManager.LoadData();
        int amountOfMap = SceneManager.sceneCountInBuildSettings - SCREEN_SYSTEM;
        for (int i = 0; i < amountOfMap; i++)
        {
            GameObject mapObj = Instantiate(mapPrefab, containMap.transform.position, Quaternion.identity);
            mapObj.transform.SetParent(containMap.transform, false);
            mapObj.GetComponent<Map>().SetName(i + SCREEN_SYSTEM);
            maps.Add(mapObj);
            if (i == 0) mapObj.GetComponent<Map>().SetData(0, true);
        }
        if (gameData.maps == null) return;
        foreach (var map in gameData.maps)
        {
            int mapNumber = map.Key;

            maps[mapNumber].GetComponent<Map>().SetData(gameData.maps[mapNumber].cupCount, gameData.maps[mapNumber].isUnlocked);
        }
        if (gameData.maps.Count != maps.Count && gameData.maps.Count >= 1)
        {
            int index = gameData.maps.Count - 1;
            if (gameData.maps[index].isUnlocked && gameData.maps[index].cupCount >= 1)
            {
                maps[index + 1].GetComponent<Map>().SetData(0, true);
            }
        }
    }
}
