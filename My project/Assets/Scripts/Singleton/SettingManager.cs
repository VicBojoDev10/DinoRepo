using UnityEngine;
using NaughtyAttributes;

public class SettingManager : MonoBehaviour
{
    [Header("SFX")]
    [ReadOnly,SerializeField] private float sfxVolume = 1f;
    [ReadOnly, SerializeField] private float musicVolume = 1f;
    public static SettingManager Instance {get; private set;}
    private void Awake()
    {
        if(Instance != null &&  Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void SetMusicValue(float value)
    {
        musicVolume = value;
    }
    public void SetSFXValue(float value)
    {
        sfxVolume = value;
    }
}
