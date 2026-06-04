using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using DG.Tweening;
using Vic.Code;

public class GameplayController : MonoBehaviour
{
   public static GameplayController Instance { get; private set; }
 
    public enum GameState { Intro, Menu, Playing, GameOver }
    public GameState currentState = GameState.Menu;
 
    [Header("UIs — locales de escena")]
    [SerializeField] private MenuUI     menuUI;
    [SerializeField] private GameplayUI gameplayUI;
    [SerializeField] private RetryUI    retryUI;
 
    [Header("Duración intro (segundos)")]
    [SerializeField] private float introAnimationDuration = 2.5f;

    private PlayerManager    _playerManager;
    private PlayerController _playerController;
 
    private float _startTime;
    private float _distanceTraveled;
    private int   _earnedEscamas;

 
    private void Awake()
    {
        Instance = this;
 
        if (GameManager.Instance != null)
        {
            _playerManager    = GameManager.Instance.playerManager;
            _playerController = GameManager.Instance.playerController;
        }
        else
        {
            Debug.LogError("[GameplayController] GameManager no encontrado.");
        }
    }
 
    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
 
    private void Start()
    {
        _playerManager?.ResetForNewGame();
 
        currentState = GameState.Menu;
        menuUI.Show();
        _playerController?.ForceMenuIdle();
        Dino.Utility.Audio.AudioManager.Instance?.PlayBGMLobby();
    }
 
    private void Update()
    {
        if (currentState == GameState.Playing)
            _distanceTraveled += 6f * Time.deltaTime;
    }

 
    public void StartGameSequence()
    {
        if (currentState != GameState.Menu) return;
 
        currentState = GameState.Intro;
        gameplayUI.Show();
        _playerController?.PlayStartIntro();
 
        StartCoroutine(IntroSequenceRoutine());
    }
 
    private IEnumerator IntroSequenceRoutine()
    {
        yield return new WaitForSeconds(introAnimationDuration);
 
        currentState      = GameState.Playing;
        _startTime        = Time.time;
        _distanceTraveled = 0f;
 
        _playerManager?.EnablePhysics();
        _playerController?.SetRunning(true);
 
        PlatformSpawner.Instance?.StartSpawning();
        Dino.Utility.Audio.AudioManager.Instance?.PlayBGMGameplay();
 
        Debug.Log("[GameplayController] Gameplay activo.");
    }
 
    public void TriggerGameOver()
    {
        if (currentState == GameState.GameOver) return;
        currentState = GameState.GameOver;
 
        PlatformSpawner.Instance?.StopSpawning();
        _playerController?.TriggerDeath();
        Dino.Utility.Audio.AudioManager.Instance?.PlayGameOver();
 
        float timeAlive = Time.time - _startTime;
        CalculateReward(timeAlive, _distanceTraveled);
 
        gameplayUI.Hide();
        retryUI.Show();
    }
 
    private void CalculateReward(float time, float distance)
    {
        _earnedEscamas = Mathf.FloorToInt(distance / 10f) + Mathf.FloorToInt(time / 5f);
        Debug.Log($"[GameplayController] {time:F1}s · {distance:F1}m · {_earnedEscamas} Escamas.");
    }
 
    public void ReloadScene()
    {
     
        DOTween.KillAll();
 
        menuUI?.HideImmediate();
        gameplayUI?.HideImmediate();
        retryUI?.HideImmediate();
 
        // 3. Recargar
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}