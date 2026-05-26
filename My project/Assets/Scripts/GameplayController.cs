using UnityEngine;
using UnityEngine.SceneManagement;
using Vic.Code;

public class GameplayController : MonoBehaviour
{
    public static GameplayController Instance { get; private set; }

    public enum GameState { Menu, Playing, GameOver }
    public GameState currentState = GameState.Menu;

    [Header("Referencias")]
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private GameObject initialPlatform;

    [Header("UIs")]
    [SerializeField] private MenuUI menuUI;
    [SerializeField] private GameplayUI gameplayUI;
    [SerializeField] private RetryUI retryUI;

    [Header("Estadísticas para Recompensas")]
    private float startTime;
    private float distanceTraveled;
    private int earnedEscamas;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentState = GameState.Menu;
        menuUI.Show();
        
        playerManager.SetPhysicsActive(false);
    }

    private void Update()
    {
        if (currentState == GameState.Playing)
        {
            distanceTraveled += 6f * Time.deltaTime; 
        }
    }

    public void StartGameSequence()
    {
        currentState = GameState.Playing;
        startTime = Time.time;
        distanceTraveled = 0f;
        
        playerManager.SetPhysicsActive(true);
        PlatformSpawner.Instance.StartSpawning();
        
        gameplayUI.Show();
        Debug.Log("Juego Iniciado - Físicas activas");
    }

    public void TriggerGameOver()
    {
        if (currentState == GameState.GameOver) return;
        currentState = GameState.GameOver;
        
        playerManager.SetPhysicsActive(false);
        PlatformSpawner.Instance.StopSpawning();
        
        float timeAlive = Time.time - startTime;
        CalculateReward(timeAlive, distanceTraveled);

        gameplayUI.Hide();
        retryUI.Show();
    }

    private void CalculateReward(float time, float distance)
    {
        earnedEscamas = Mathf.FloorToInt(distance / 10f) + Mathf.FloorToInt(time / 5f);
        Debug.Log($"Has sobrevivido {time:F1}s y recorrido {distance:F1}m. Ganaste: {earnedEscamas} Escamas.");
        
    }
    

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}