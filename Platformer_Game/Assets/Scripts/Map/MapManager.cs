using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public void SelectMap(int mapNumber)
    {
        StartCoroutine(LoadYourAsyncScene(mapNumber));
    }
    IEnumerator LoadYourAsyncScene(int mapNumber)
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(mapNumber);


        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
