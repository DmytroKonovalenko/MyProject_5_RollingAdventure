using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioSource soundEffect;
    [SerializeField] private AudioSource backgroundMusic;

    private const string SoundVolumeKey = "SoundVolume";
    private const string MusicVolumeKey = "MusicVolume";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        LoadAudioSettings();
    }

    public void ToggleSound()
    {
        soundEffect.volume = soundEffect.volume == 1 ? 0 : 1;
        SaveAudioSettings();
    }

    public void ToggleMusic()
    {
        backgroundMusic.volume = backgroundMusic.volume == 1 ? 0 : 1;
        SaveAudioSettings();
    }

    public float GetSoundVolume() => soundEffect.volume;
    public float GetMusicVolume() => backgroundMusic.volume;

    private void SaveAudioSettings()
    {
        PlayerPrefs.SetFloat(SoundVolumeKey, soundEffect.volume);
        PlayerPrefs.SetFloat(MusicVolumeKey, backgroundMusic.volume);
        PlayerPrefs.Save();
    }
    

    private void LoadAudioSettings()
    {
        soundEffect.volume = PlayerPrefs.HasKey(SoundVolumeKey) ? PlayerPrefs.GetFloat(SoundVolumeKey) : 1;
        backgroundMusic.volume = PlayerPrefs.HasKey(MusicVolumeKey) ? PlayerPrefs.GetFloat(MusicVolumeKey) : 1;
    }
    public void PlaySoundEffect()
    {
        soundEffect.Play();
    }
}
