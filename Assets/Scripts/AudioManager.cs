using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep the AudioManager across scenes
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = gameObject.AddComponent<AudioSource>();
    }


    // Play a clip as a one-shot sound effect
    public void PlayClipOneShot(AudioClip clip, float volume = 1.0f)
    {
        if (clip == null)
        {
            Debug.LogWarning("AudioManager: No audio clip provided to play.");
            return;
        }

        audioSource.PlayOneShot(clip, volume);
    }


}
