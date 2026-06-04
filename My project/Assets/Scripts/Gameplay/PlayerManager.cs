using UnityEngine;
using UnityEngine.InputSystem;
using Vic.Code;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [Header("Físicas")] [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float gravityScaleMultiplier = 3f;

    [Header("Detección de suelo")] [SerializeField]
    private Transform groundCheck;

    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Punto de Spawn")] [SerializeField]
    private Transform spawnPoint;

    [Header("Configuración del Slash")] [SerializeField]
    private Transform slashCenter;

    [SerializeField] private float slashRadius = 1.2f;
    [SerializeField] private LayerMask enemyLayer;

    private Rigidbody2D _rb;
    private bool _physicsEnabled;
    private bool _isGrounded;
    private bool _desiresToJump;
    private bool _desiresToHighJump;

    public bool IsSlashing { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _rb = GetComponent<Rigidbody2D>();
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _rb.freezeRotation = true;
        _rb.gravityScale = 0f;
    }

    private void FixedUpdate()
    {
        if (!_physicsEnabled) return;

        _isGrounded = groundCheck != null &&
                      Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (_desiresToJump)
        {
            if (_isGrounded)
            {
                float finalJumpForce = _desiresToHighJump ? jumpForce * 1.6f : jumpForce;
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, finalJumpForce);
            }

            _desiresToJump = false;
            _desiresToHighJump = false;
        }
    }

    public void EnablePhysics()
    {
        _physicsEnabled = true;
        _rb.gravityScale = gravityScaleMultiplier;
    }

    public void ResetForNewGame()
    {
        _physicsEnabled = false;
        _rb.gravityScale = 0f;
        _rb.linearVelocity = Vector2.zero;
        IsSlashing = false;

        if (spawnPoint != null) transform.position = spawnPoint.position;
    }

    public void Jump(bool isHighJump = false)
    {
        if (!_physicsEnabled) return;
        _desiresToJump = true;
        _desiresToHighJump = isHighJump;
    }

    public void ExecuteSlash()
    {
        if (!_physicsEnabled) return;

        IsSlashing = true;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(slashCenter.position, slashRadius, enemyLayer);

        foreach (Collider2D enemyCollider in hitEnemies)
        {

            EnemyEntity enemy = enemyCollider.GetComponent<EnemyEntity>();
            if (enemy != null)
            {
                enemy.OnHitBySlash();
            }
        }

        Invoke(nameof(EndSlash), 0.15f);
    }

    private void EndSlash()
        {
            IsSlashing = false;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("DeathZone"))
            {
                GameplayController.Instance?.TriggerGameOver();
                ResetForNewGame();
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (groundCheck != null)
            {
                Gizmos.color = _isGrounded ? Color.green : Color.red;
                Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
            }

            if (slashCenter != null)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireSphere(slashCenter.position, slashRadius);
            }
        }
#endif
    }

