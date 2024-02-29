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
        diedPanel.SetActive(true);
    }
}
