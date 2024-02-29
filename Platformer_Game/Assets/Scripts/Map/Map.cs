using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    [SerializeField] private GameObject[] cups;
    [SerializeField] private TMP_Text mapNumberText;
    [SerializeField] private GameObject lockPanel;
    bool isUnclocked;
    int mapNumber = 0;
    const string soundTap = "Tap";
    public void SetName(int mapNumber)
    {
        mapNumberText.text = "Map" + (mapNumber-2).ToString();
        this.mapNumber = mapNumber;
    }
    public void SetData(int amountOfCup,bool isUnclocked)
    {
        for (int i = 0; i < amountOfCup; i++) 
        {
            Color color = cups[i].GetComponent<Image>().color;
            color.a = 255;
            cups[i].GetComponent<Image>().color = color;
        }
        lockPanel.SetActive(!isUnclocked);
        this.isUnclocked = isUnclocked;
    }
    public void MoveToMapAsync()
    {
        if (!isUnclocked) return;
        SoundManager.Instance.PlaySound(soundTap);
        StartCoroutine(LoadMapAsync(mapNumber));
    }

    private IEnumerator LoadMapAsync(int mapBuildIndex)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(mapBuildIndex);

        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
