using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public RecordDatabase recordDatabase;
    public AudioSource audioSource;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject); // Optional if you have multiple scenes
    }

    public void setAudioClip(AudioClip audioClip)
    {
        if (audioSource != null && audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("AudioSource or AudioClip is null.");
        }
    }
}
