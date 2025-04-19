using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Mixer")]
    public AudioMixer mixer;               // drag in GameAudioMixer asset

    [Header("Sources")]
    public AudioSource musicSource;        // drag in the AudioSource on this GameObject
    public AudioSource sfxSource;   

    void Awake()
    {
        // singleton pattern
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            return;
        }

        // load saved volume (or default to 0.75)
        float mVol = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        float sVol = PlayerPrefs.GetFloat("SFXVolume",   0.75f);

        SetMusicVolume(mVol);
        SetSFXVolume(sVol);
    }

    // Play background music (call once at scene start)
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    // Helper to play one‑shot SFX (at camera position)
    public void PlaySFX(AudioClip clip, float volumeScale = 1f)
{
    if (clip == null || sfxSource == null) return;
        sfxSource.PlayOneShot(clip, volumeScale*10f);
}

    // Called by UI slider (0…1)
    public void SetMusicVolume(float sliderValue)
    {
        float dB = Mathf.Log10(Mathf.Clamp(sliderValue, 0.001f, 1f)) * 20f;
        Debug.LogWarning("Audio value: " + dB);
    if (!mixer.SetFloat("MusicVolume", dB))
        Debug.LogWarning("AudioMixer parameter ‘MusicVolume’ not found or snapshots are being edited.");
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }

    // Called by UI slider (0…1)
    public void SetSFXVolume(float sliderValue)
    {
        float dB = Mathf.Log10(Mathf.Clamp(sliderValue, 0.001f, 1f)) * 20f;
        mixer.SetFloat("SFXVolume", dB);
        PlayerPrefs.SetFloat("SFXVolume", sliderValue);
    }
}
