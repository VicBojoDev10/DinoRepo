using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("BGM Audio Sources (Música)")]
    public AudioSource bgmLobby;
    public AudioSource bgmUpgrades;
    public AudioSource bgmGameplay;

    [Header("SFX Audio Source (Efectos)")]
    public AudioSource sfxSource;

    [Header("SFX - Menús")]
    public AudioClip sfxEnterMenu;
    public AudioClip sfxBackMenu;

    [Header("SFX - Gameplay (Drako)")]
    public AudioClip sfxInitialHit; 
    public AudioClip sfxJump;
    public AudioClip sfxSlash; 
    public AudioClip sfxRetry;
    public AudioClip sfxGameOver;

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
        }
    }

    private void Start()
    {
        PlayBGMLobby();
    }
    
    public void PlayBGMLobby() => SwitchBGM(bgmLobby);
    public void PlayBGMUpgrades() => SwitchBGM(bgmUpgrades);
    public void PlayBGMGameplay() => SwitchBGM(bgmGameplay);

    private void SwitchBGM(AudioSource targetSource)
    {
        if (bgmLobby != targetSource) bgmLobby.Pause();
        if (bgmUpgrades != targetSource) bgmUpgrades.Pause();
        if (bgmGameplay != targetSource) bgmGameplay.Pause();
        
        if (!targetSource.isPlaying)
        {
            targetSource.Play();
        }
    }
    
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
    
    public void PlayEnterUI() => PlaySFX(sfxEnterMenu);
    public void PlayBackUI() => PlaySFX(sfxBackMenu);
    public void PlayInitialHit() => PlaySFX(sfxInitialHit);
    public void PlayJump() => PlaySFX(sfxJump);
    public void PlaySlash() => PlaySFX(sfxSlash);
    public void PlayRetry() => PlaySFX(sfxRetry);
    public void PlayGameOver() => PlaySFX(sfxGameOver);
}
