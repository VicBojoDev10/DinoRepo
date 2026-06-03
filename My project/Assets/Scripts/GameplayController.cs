using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Vic.Code;

public class GameplayController : MonoBehaviour
{
   public static GameplayController Instance { get; private set; }
 
    public enum GameState { Intro, Menu, Playing, GameOver }
    public GameState currentState = GameState.Intro;
 
    [Header("Referencias — objetos de esta misma escena")]
    [SerializeField] private PlayerManager    playerManager;
    [SerializeField] private PlayerController playerController;
 
    [Header("UIs")]
    [SerializeField] private MenuUI     menuUI;
    [SerializeField] private GameplayUI gameplayUI;
    [SerializeField] private RetryUI    retryUI;
 
    [Header("Animación de Inicio")]
    [SerializeField] private float introAnimationDuration = 2.5f;
 
    private float _startTime;
    private float _distanceTraveled;
    private int   _earnedEscamas;

 
    private void Awake()
    {
        Instance = this;
    }
 
    private void OnDestroy()
    {
   
        if (Instance == this) Instance = null;
    }
 
    private void Start()
    {
        currentState = GameState.Menu;
        menuUI.Show();
 
        if (playerController != null)
            playerController.ForceMenuIdle();
 
        AudioManager.Instance?.PlayBGMLobby();
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
 
        if (playerController != null)
            playerController.PlayStartIntro();
 
        StartCoroutine(IntroSequenceRoutine());
    }
 
    private IEnumerator IntroSequenceRoutine()
    {
        yield return new WaitForSeconds(introAnimationDuration);
 
        currentState      = GameState.Playing;
        _startTime        = Time.time;
        _distanceTraveled = 0f;
 
        playerManager.EnablePhysics();
 
        if (playerController != null)
            playerController.SetRunning(true);
 
        PlatformSpawner.Instance.StartSpawning();
        AudioManager.Instance?.PlayBGMGameplay();
 
        Debug.Log("[GameplayController] Gameplay activo.");
    }
 
    public void TriggerGameOver()
    {
        if (currentState == GameState.GameOver) return;
        currentState = GameState.GameOver;
 
        PlatformSpawner.Instance?.StopSpawning();
 
        if (playerController != null)
            playerController.TriggerDeath();
 
        AudioManager.Instance?.PlayGameOver();
 
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}