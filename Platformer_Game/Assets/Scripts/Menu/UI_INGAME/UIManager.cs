using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject diedPanel;
    public void Start()
    {
        diedPanel.SetActive(false);
    }
    private void OnEnable()
    {
        Health.OnPlayerDied += HandleAfterPlayerDied;
    }
    private void OnDisable()
    {
        Health.OnPlayerDied -= HandleAfterPlayerDied;
    }
    public void HandleAfterPlayerDied()
    {
        StartCoroutine(DelayPanel());
    }
    private IEnumerator DelayPanel()
    {
        yield return new WaitForSeconds(0.4f);
        diedPanel.SetActive(true);
    }
}
