using UnityEngine;
using NaughtyAttributes;
using System.IO;

public class SettingManager : MonoBehaviour
{
     public static SettingManager Instance { get; private set; }

    [Header("Valores actuales (solo lectura)")]
    [ReadOnly, SerializeField] private float sfxVolume   = 1f;
    [ReadOnly, SerializeField] private float musicVolume = 1f;

    public float MusicSliderUI => musicVolume;
    public float SfxSliderUI   => sfxVolume;
 
    private static string SavePath => Path.Combine(Application.persistentDataPath, "settings.json");

    #region Unity Lifecycle
 
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
 
        Instance = this;
        DontDestroyOnLoad(gameObject);
 
        LoadSettings();
    }
    private void Start()
    {
        ApplyToAudioManager();
    }
 
    #endregion
    
    #region Public Setters (llamados desde los sliders de la UI)
 
    public void SetMusicValue(float value)
    {
        musicVolume = Mathf.Clamp01(value);
        Dino.Utility.Audio.AudioManager.Instance?.SetMusicVolume(musicVolume);
        SaveSettings();
    }
 
    public void SetSfxValue(float value)
    {
        sfxVolume = Mathf.Clamp01(value);
        Dino.Utility.Audio.AudioManager.Instance?.SetSfxVolume(sfxVolume);
        SaveSettings();
    }
 
    #endregion

    #region Persistence (JSON)
 
    private void LoadSettings()
    {
        if (File.Exists(SavePath))
        {
            try
            {
                string json = File.ReadAllText(SavePath);
                SettingsData data = JsonUtility.FromJson<SettingsData>(json);
                sfxVolume   = Mathf.Clamp01(data.sfxVolume);
                musicVolume = Mathf.Clamp01(data.musicVolume);
                Debug.Log($"[SettingManager] Ajustes cargados — Música: {musicVolume:F2}  SFX: {sfxVolume:F2}");
            }
            catch (System.Exception e)
            {
                Debug.LogWarning($"[SettingManager] Error leyendo settings.json, usando valores por defecto. ({e.Message})");
                ResetToDefaults();
            }
        }
        else
        {
            Debug.Log("[SettingManager] No existe settings.json, creando con valores por defecto.");
            ResetToDefaults();
            SaveSettings();
        }
    }
 
    private void SaveSettings()
    {
        SettingsData data = new SettingsData
        {
            sfxVolume   = sfxVolume,
            musicVolume = musicVolume
        };
 
        try
        {
            File.WriteAllText(SavePath, JsonUtility.ToJson(data, prettyPrint: true));
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[SettingManager] No se pudo guardar settings.json: {e.Message}");
        }
    }
 
    private void ResetToDefaults()
    {
        sfxVolume   = 1f;
        musicVolume = 1f;
    }
 
    #endregion
    
    #region Helpers
    
    private void ApplyToAudioManager()
    {
        Dino.Utility.Audio.AudioManager.Instance?.SetMusicVolume(musicVolume);
        Dino.Utility.Audio.AudioManager.Instance?.SetSfxVolume(sfxVolume);
    }
 
    #endregion
}

public static class PlayerPrefsKeys
{
    public const string sfxVolume   = "SFXVolume";
    public const string musicVolume = "MusicVolume";
}
 