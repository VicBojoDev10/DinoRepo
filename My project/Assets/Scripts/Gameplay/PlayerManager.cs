using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Vic.Code;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
 
    [Header("Físicas")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float gravity   = 30f;
 
    [Header("Detección de suelo")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float     groundCheckRadius = 0.15f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Sistema de Vidas")]
    [SerializeField] private int maxLives = 2;
    private int _currentLives;

    [Header("Ataque Slash")]
    [SerializeField] private Transform slashCenter;
    [SerializeField] private float     slashRadius = 1.5f;
    [SerializeField] private LayerMask enemyLayer;
    
    public bool IsSlashing { get; private set; }
 
    private Rigidbody2D _rb;
    private PlayerController _playerController;
    private float       _verticalVelocity;
    private bool        _physicsEnabled;
    private bool        _isGrounded;

    private Vector3 _InitialSpawnPosition;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
 
        _rb = GetComponent<Rigidbody2D>();
        _playerController = GetComponent<PlayerController>();

        _rb.bodyType       = RigidbodyType2D.Dynamic;
        _rb.gravityScale   = 0f;  
        _rb.freezeRotation = true;

        _currentLives = maxLives;
        _InitialSpawnPosition = transform.position;
    }
 
    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
 
    private void FixedUpdate()
    {
        if (transform.rotation.z != 0f)
        {
            transform.rotation = Quaternion.identity;
        }

        if (!_physicsEnabled) 
        {
            _rb.linearVelocity = Vector2.zero;
            return;
        }
 

        _isGrounded = groundCheck != null &&
                      Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
 

        if (_isGrounded)
        {
 
            if (_verticalVelocity <= 0f)
            {
                _verticalVelocity = -0.1f; 
            }
        }
        else
        {
            _verticalVelocity -= gravity * Time.fixedDeltaTime;
        }
        
        _rb.linearVelocity = new Vector2(0f, _verticalVelocity);
    }
    
    public void StartGameSequenceFromMenu()
    {
        _physicsEnabled = false;
        _verticalVelocity = 0f;
        _rb.linearVelocity = Vector2.zero;
        _currentLives = maxLives;
        
        if (_playerController != null)
        {
            _playerController.PlayStartIntro();
        }
        StartCoroutine(IntroSequenceRoutine());
    }

    private IEnumerator IntroSequenceRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        
        EnablePhysics();
    }

    public void EnablePhysics()
    {
        _verticalVelocity = 0f; 
        _physicsEnabled   = true;
        
        if (_playerController != null)
        {
            _playerController.SetRunning(true);
        }
    }
    
    public void ResetForNewGame()
    {
        Time.timeScale = 1f;

        
        transform.position = _InitialSpawnPosition;
        transform.rotation = Quaternion.identity; 
        
        _physicsEnabled    = false;
        _verticalVelocity  = 0f;
        _rb.linearVelocity = Vector2.zero;
        _currentLives      = maxLives;
        IsSlashing         = false;

        
        if (_playerController != null)
        {
            _playerController.ForceMenuIdle();
        }
    }
 
    public void Jump(bool isHighJump = false)
    {
        if (!_physicsEnabled) return;
        
        if (_isGrounded)
        {
            _verticalVelocity = isHighJump ? jumpForce * 1.6f : jumpForce;
            if (_playerController != null) _playerController.TriggerJump();
        }
    }
    
    public void TakeDamage()
    {
        if (!_physicsEnabled) return;

        _currentLives--;

        if (_playerController != null)
        {
            _playerController.TriggerDamage();
        }

        if (_currentLives > 0)
        {
            StartCoroutine(DamageRecoveryRoutine());
        }
        else
        {
            _physicsEnabled = false;
            _verticalVelocity = 0f;
            _rb.linearVelocity = Vector2.zero;

            if (_playerController != null)
            {
                _playerController.TriggerDeath();
            }
            
            Time.timeScale = 0f;

            ReviveUI reviveUI = Object.FindFirstObjectByType<ReviveUI>();
            if (reviveUI != null)
            {
                reviveUI.Show();
            }
            else
            {
                Time.timeScale = 1f;
                GameplayController.Instance?.TriggerGameOver();
            }
        }
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        _physicsEnabled = false;
        _verticalVelocity = 0f;
        _rb.linearVelocity = Vector2.zero;
        
        if (_playerController != null) _playerController.SetRunning(false);

        yield return new WaitForSeconds(0.6f); 

        if (_currentLives > 0)
        {
            EnablePhysics(); 
            
        }
    }
    
    public void ActionRevive()
    {
        Time.timeScale = 1f;

        _currentLives = 1; 
        _verticalVelocity = 0f;
        _rb.linearVelocity = Vector2.zero;

        if (_playerController != null)
        {
            _playerController.TriggerRevive();
        }

        EnablePhysics();
    }
 
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("DeathZone"))
        {
            FallToDeathZone();
        }
    }

    private void FallToDeathZone()
    {
        _physicsEnabled = false;
        _verticalVelocity = 0f;
        _rb.linearVelocity = Vector2.zero;
        
        _currentLives = 0;

       
        Transform menuSpawnPoint = GameplayController.Instance != null ? GameplayController.Instance.transform : null;
        if (menuSpawnPoint != null)
        {
            transform.position = menuSpawnPoint.position;
        }
        else
        {
            transform.position = new Vector3(0f, 0f, 0f); 
        }

        if (_playerController != null)
        {
            _playerController.ForceMenuIdle();
        }


        GameplayController.Instance?.TriggerGameOver();

        {
            RetryUI retryUI = Object.FindFirstObjectByType<RetryUI>();
            if (retryUI != null)
            {
                retryUI.Show();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
        }
    }
    public void ExecuteSlash()
    {
        if (!_physicsEnabled) return;

        IsSlashing = true;
        if (_playerController != null) _playerController.TriggerSlash();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(slashCenter.position, slashRadius, enemyLayer);
        foreach (Collider2D enemyCollider in hitEnemies)
        {
            EnemyEntity regularEnemy = enemyCollider.GetComponent<EnemyEntity>();
            if (regularEnemy != null) regularEnemy.OnHitBySlash();
            
            SpawningEnemy visualEnemy = enemyCollider.GetComponent<SpawningEnemy>();
            if (visualEnemy != null) visualEnemy.OnHitBySlash();
        }

        // Programamos el fin del ataque tras unos frames
        Invoke(nameof(EndSlash), 0.15f);
    }

    private void EndSlash()
    {
        IsSlashing = false;
    }
 
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = _isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
#endif
    }


