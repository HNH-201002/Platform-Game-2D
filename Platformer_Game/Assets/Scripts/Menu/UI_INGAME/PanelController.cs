using UnityEngine;

public class PanelController : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    const string soundTap = "Tap";
    private static bool isPaused = false;
    public void Start()
    {
        Time.timeScale = 1;
    }
    public void ButtonEvent()
    {
        isPaused = !isPaused;
        panel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0.0f : 1.0f;
        SoundManager.Instance.PlaySound(soundTap);
    }

}
