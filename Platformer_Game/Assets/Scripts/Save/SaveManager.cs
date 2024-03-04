using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class MapData
{
    public int mapNumber;
    public int cupCount;
    public bool isUnlocked;

    public MapData(int number, int cups, bool unlocked)
    {
        mapNumber = number;
        cupCount = cups;
        isUnlocked = unlocked;
    }
}
[System.Serializable] 
public class MapDataListWrapper
{
    public List<MapData> mapDataList;
}
public class GameData
{
    public Dictionary<int,MapData> maps; 
    public GameData()
    {
        maps = new Dictionary<int, MapData>();
    }
}
public class SaveManager : MonoBehaviour
{

    public static void SaveData(MapData data)
    {
        GameData gameData = LoadData();
        string path = Application.persistentDataPath + "/saveFile.json";
        if (gameData.maps.ContainsKey(data.mapNumber))
        {
            gameData.maps[data.mapNumber] = data;
        }
        else
        {
            gameData.maps.Add(data.mapNumber, data);
        }

        MapDataListWrapper wrapper = new MapDataListWrapper
        {
            mapDataList = new List<MapData>(gameData.maps.Values)
        };

        string json = JsonUtility.ToJson(wrapper, true);

        FileInfo file = new FileInfo(path);
        file.Directory.Create();

        File.WriteAllText(path, json);
        Debug.Log("Data saved to " + path);
    }
    public static GameData LoadData()
    {
        string path = Application.persistentDataPath + "/saveFile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            MapDataListWrapper wrapper = JsonUtility.FromJson<MapDataListWrapper>(json);
            GameData gameData = new GameData();
            foreach (var mapData in wrapper.mapDataList)
            {
                gameData.maps.Add(mapData.mapNumber, mapData);
            }
            return gameData;
        }
        else
        {
            return new GameData();
        }
    }
}
