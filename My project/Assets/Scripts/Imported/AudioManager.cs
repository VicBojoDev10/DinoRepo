using System.Collections.Generic;
using UnityEngine;

namespace Dino.Utility.Audio
{

    /// <summary>
    /// Last update 14/03/2025 Dino
    /// A class that allows you to manage audio.
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [Header("BGM — deben ser hijos de este GameObject")] [SerializeField]
        private AudioSource bgmLobby;

        [SerializeField] private AudioSource bgmUpgrades;
        [SerializeField] private AudioSource bgmGameplay;

        [Header("SFX — Menús")] [SerializeField]
        private AudioClip sfxEnterMenu;

        [SerializeField] private AudioClip sfxBackMenu;

        [Header("SFX — Gameplay")] [SerializeField]
        private AudioClip sfxInitialHit;

        [SerializeField] private AudioClip sfxJump;
        [SerializeField] private AudioClip sfxSlash;
        [SerializeField] private AudioClip sfxRetry;
        [SerializeField] private AudioClip sfxGameOver;

        private float _musicVolume = 1f;
        private float _sfxVolume = 1f;

        private GameObject _sfxContainer;
        private List<AudioSource> _sfxPool = new List<AudioSource>();
        private AudioSource _currentBGM;


        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitSfxContainer();
        }

        private void Start()
        {
            if (SettingManager.Instance != null)
            {
                SetMusicVolume(SettingManager.Instance.MusicSliderUI);
                SetSfxVolume(SettingManager.Instance.SfxSliderUI);
            }
            
            if (_currentBGM == null || !_currentBGM.isPlaying)
                PlayBGMLobby();
        }



        public void SetMusicVolume(float value)
        {
            _musicVolume = Mathf.Clamp01(value);
            bgmLobby.volume = _musicVolume;
            bgmUpgrades.volume = _musicVolume;
            bgmGameplay.volume = _musicVolume;
        }

        public void SetSfxVolume(float value)
        {
            _sfxVolume = Mathf.Clamp01(value);
            foreach (AudioSource src in _sfxPool)
                if (src != null)
                    src.volume = _sfxVolume;
        }


        public void PlayBGMLobby() => SwitchBGM(bgmLobby);
        public void PlayBGMUpgrades() => SwitchBGM(bgmUpgrades);
        public void PlayBGMGameplay() => SwitchBGM(bgmGameplay);

        private void SwitchBGM(AudioSource target)
        {
            if (_currentBGM == target && target.isPlaying) return;

            if (bgmLobby != target) bgmLobby.Pause();
            if (bgmUpgrades != target) bgmUpgrades.Pause();
            if (bgmGameplay != target) bgmGameplay.Pause();

            target.volume = _musicVolume;
            if (!target.isPlaying) target.Play();
            _currentBGM = target;
        }

        public void PlayEnterUI() => PlaySFX(sfxEnterMenu);
        public void PlayBackUI() => PlaySFX(sfxBackMenu);
        public void PlayInitialHit() => PlaySFX(sfxInitialHit);
        public void PlayJump() => PlaySFX(sfxJump);
        public void PlaySlash() => PlaySFX(sfxSlash);
        public void PlayRetry() => PlaySFX(sfxRetry);
        public void PlayGameOver() => PlaySFX(sfxGameOver);

        public void PlaySFX(AudioClip clip)
        {
            if (clip == null) return;
            AudioSource src = GetFreeSfxSource();
            src.clip = clip;
            src.volume = _sfxVolume;
            src.Play();
        }

        private AudioSource GetFreeSfxSource()
        {
            foreach (AudioSource src in _sfxPool)
                if (src != null && !src.isPlaying)
                    return src;
            return CreateSfxSource();
        }

        private AudioSource CreateSfxSource()
        {
            GameObject go = new GameObject("SFX_Source");
            go.transform.SetParent(_sfxContainer.transform);
            AudioSource src = go.AddComponent<AudioSource>();
            src.playOnAwake = false;
            src.loop = false;
            src.volume = _sfxVolume;
            _sfxPool.Add(src);
            return src;
        }

        private void InitSfxContainer()
        {

            Transform existing = transform.Find("SFX_Container");
            if (existing != null)
            {
                _sfxContainer = existing.gameObject;
     
                _sfxPool.Clear();
                _sfxPool.AddRange(_sfxContainer.GetComponentsInChildren<AudioSource>());
            }
            else
            {
                _sfxContainer = new GameObject("SFX_Container");
                _sfxContainer.transform.SetParent(transform);
            }
        }
    }

    namespace Dino.Utility.Audio
    {
        public enum AudioType
        {
            Music,
            SFX,
            Ambience
        }
    }
}