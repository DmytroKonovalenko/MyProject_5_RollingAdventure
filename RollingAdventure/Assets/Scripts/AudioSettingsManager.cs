using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsManager : MonoBehaviour
{
    [SerializeField] private Button soundToggleButton;
    [SerializeField] private Button musicToggleButton;

    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;

    [SerializeField] private Sprite musicOnSprite;
    [SerializeField] private Sprite musicOffSprite;

    private void Start()
    {
        UpdateSoundButtonIcon();
        UpdateMusicButtonIcon();

     //   soundToggleButton.onClick.AddListener(ToggleSound);
     //   musicToggleButton.onClick.AddListener(ToggleMusic);
    }

    public void ToggleSound()
    {
        SoundManager.Instance.ToggleSound();
        UpdateSoundButtonIcon();
    }

    public void ToggleMusic()
    {
        SoundManager.Instance.ToggleMusic();
        UpdateMusicButtonIcon();
    }

    private void UpdateSoundButtonIcon()
    {
        soundToggleButton.image.sprite = SoundManager.Instance.GetSoundVolume() == 1 ? soundOnSprite : soundOffSprite;
    }

    private void UpdateMusicButtonIcon()
    {
        musicToggleButton.image.sprite = SoundManager.Instance.GetMusicVolume() == 1 ? musicOnSprite : musicOffSprite;
    }
}
