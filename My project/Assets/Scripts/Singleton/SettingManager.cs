using UnityEngine;
using NaughtyAttributes;

public class SettingManager : MonoBehaviour
{
    [Header("SFX")] [ReadOnly, SerializeField] private float sfxVolume = 1f;

    [ReadOnly, SerializeField] private float musicVolume = 1f;
    public static SettingManager Instance { get; private set; }
    
    public float MusicSliderUI => musicVolume;
    public float SfxSliderUI => sfxVolume;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
        ReadSettings();
    }

    private void ReadSettings()
    {
        sfxVolume = PlayerPrefs.GetFloat(PlayerPrefsKeys.sfxVolume);
        musicVolume = PlayerPrefs.GetFloat(PlayerPrefsKeys.musicVolume);
        Debug.Log("sfxVolume: " + sfxVolume +  " musicVolume: " + musicVolume);
    }

    public void SetMusicValue(float value)
    {
        musicVolume = value;
        PlayerPrefs.SetFloat(PlayerPrefsKeys.musicVolume, musicVolume);
    }

    public void SetSfxValue(float value)
    {
        sfxVolume = value;
        PlayerPrefs.SetFloat(PlayerPrefsKeys.sfxVolume, sfxVolume);
    }

    private void SaveSettings()
    {
        
    }
}
    public static class PlayerPrefsKeys
    {
        public const string sfxVolume = "SFXVolume";
        public const string musicVolume = "MusicVolume";
    }
