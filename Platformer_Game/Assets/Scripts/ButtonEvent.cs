using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour
{
    const string soundTap = "Tap";
    public void LoadNextSceneAsync()
    {
        SoundManager.Instance.PlaySound(soundTap);
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        int nextSceneIndex = currentSceneIndex + 1;

        int sceneToLoad = (nextSceneIndex < SceneManager.sceneCountInBuildSettings) ? nextSceneIndex : 0; 

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
