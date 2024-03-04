using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider sfxSlider;

    private const string MUSIC_VOLUME_PREF = "MusicVolume";
    private const string SFX_VOLUME_PREF = "SFXVolume";
    public void Start()
    {
        soundSlider.value = PlayerPrefs.GetFloat(MUSIC_VOLUME_PREF, 0.75f);
        sfxSlider.value = PlayerPrefs.GetFloat(SFX_VOLUME_PREF, 0.75f);
        VolumeSetting();
        VolumeSfxSetting();
    }

    public void VolumeSetting()
    {
        if (mixer != null)
        {
            float volume = soundSlider.value;
            mixer.SetFloat("Music", Mathf.Log10(volume) * 20);

            PlayerPrefs.SetFloat(MUSIC_VOLUME_PREF, volume);
        }
    }

    public void VolumeSfxSetting()
    {
        if (mixer != null)
        {
            float volume = sfxSlider.value;
            mixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat(SFX_VOLUME_PREF, volume);
        }
    }
}