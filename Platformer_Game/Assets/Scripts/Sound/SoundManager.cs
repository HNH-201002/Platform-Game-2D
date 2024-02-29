using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Member;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [SerializeField] private List<AudioClip> clips;
    private Dictionary<string, AudioClip> clipsDictionary = new Dictionary<string, AudioClip>();
    [SerializeField] private AudioMixerGroup audioMixer;
    [SerializeField] private AudioSource audioSFX;
    [SerializeField] private AudioSource audioSound;
    // Mapping of scene names to background sound names
    private Dictionary<string, string> sceneToBackgroundSound = new Dictionary<string, string>
    {
        {"M_1", "M_BG"},
        {"M_2", "M_BG"},
        {"M_3", "M_BG"},
        {"Menu","Menu_BG"}
    };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Initialize the dictionary with audio clips
            foreach (var clip in clips)
            {
                clipsDictionary.Add(clip.name, clip);
            }
            audioSound.loop = true; // Enable looping

            // Subscribe to the scene loaded event
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlaySound(string clipName, bool loop = false)
    {
        if (clipsDictionary.TryGetValue(clipName, out AudioClip clip))
        {
            //source.outputAudioMixerGroup = audioMixer;
            audioSFX.clip = clip;
            audioSFX.loop = loop;
            audioSFX.Play();
        }
        else
        {
            Debug.LogWarning("Clip not found: " + clipName);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (sceneToBackgroundSound.TryGetValue(scene.name, out string backgroundSoundName))
        {
            PlayBackgroundMusic(backgroundSoundName);
        }
        else
        {
            Debug.LogWarning("No background sound mapping found for scene: " + scene.name);
            // Optionally, play a default background sound if no mapping is found
        }
    }

    public void PlayBackgroundMusic(string backgroundSoundName)
    {
        if (clipsDictionary.TryGetValue(backgroundSoundName, out AudioClip clip))
        {
            if (audioSound.clip != clip) // Only swap and play the clip if it's different
            {
                audioSound.clip = clip;
                audioSound.Play();
            }
        }
        else
        {
            Debug.LogWarning("Background music clip not found: " + backgroundSoundName);
        }
    }


    private void OnDestroy()
    {
        // Unsubscribe from the scene loaded event to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}