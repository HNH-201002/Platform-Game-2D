using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public const string soundTap = "Tap";
    public const string soundRespawn = "Respawn";
    public string menuScreenName;
    public void TurnOffPanel()
    {
        SoundManager.Instance.PlaySound(soundTap);
        transform.parent.gameObject.SetActive(false);
    }
    public void ReloadButton()
    {
        SoundManager.Instance.PlaySound(soundTap);
        StartCoroutine(LoadSceneCoroutine(SceneManager.GetActiveScene().name));
    }
    public void Back()
    {
        SoundManager.Instance.PlaySound(soundTap);
        StartCoroutine(LoadSceneCoroutine(menuScreenName));
    }
    private IEnumerator LoadSceneCoroutine(string sceneName)
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
