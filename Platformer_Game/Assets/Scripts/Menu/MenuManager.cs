using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public const string startScreenName = "MapSelect";
    public const string shopScreenName = "Shop";
    public const string soundTap = "Tap";
    public GameObject settingPanel;
    public void StartButton()
    {
        SoundManager.Instance.PlaySound(soundTap);
        StartCoroutine(LoadSceneCoroutine(startScreenName));
    }
    public void ShopButton()
    {
        SoundManager.Instance.PlaySound(soundTap);
        StartCoroutine(LoadSceneCoroutine(shopScreenName));
    }
    public void SettingButton()
    {
        SoundManager.Instance.PlaySound(soundTap);
        settingPanel.SetActive(true);
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
