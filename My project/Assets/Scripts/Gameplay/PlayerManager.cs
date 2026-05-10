using UnityEngine;
using Vic.Code;
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [Header("Movement & Jump")] public int maxHealth = 3;
    public int currentHealth;
    public int lives = 1;

    [Header("Floor")] [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    public bool isGrounded;

    [Header("Slash & Range")]
    public float slashRange = 1.5f;
    public LayerMask enemyLayer;
    [SerializeField] private Transform attackPoint;
    
    [Header("UI")]
    [SerializeField] private GameplayUI gameplayUI;
    [SerializeField] private ReviveUI reviveUI;
    [SerializeField] private RetryUI retryUI;
    [SerializeField] private MenuUI menuUI;
    
    private PlayerController playerController;

    private void Awake()
    {
        Instance = this;
        playerController = GetComponent<PlayerController>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        CheckGroundStatus();
    }

    private void CheckGroundStatus()
    {
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        playerController.TriggerDamage();

        if (currentHealth <= 0)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        playerController.TriggerDeath();
        gameplayUI.Hide();

        if (lives > 0)
        {
            reviveUI.Show();
        }
        else
        {
            retryUI.Show();
        }
    }

    public void ActionRevive()
    {
        lives--;
        currentHealth = maxHealth;
        reviveUI.Hide();
        
        playerController.TriggerRevive();
        gameplayUI.Show();
    }

    public void ActionRetry()
    {
        retryUI.Hide();
        ResetPlayerStats();
        GameplayController.Instance.StartGameSequence();
    }

    public void ActionBackMenu()
    {
        retryUI.Hide();
        reviveUI.Hide();
        gameplayUI.Hide();
        
        playerController.ForceMenuIdle();
        menuUI.Show();
    }

    private void ResetPlayerStats()
    {
        currentHealth = maxHealth;
        lives = 1;
    }

    private void OnDrawGizmosSelected()
    {
        if(groundCheck != null)
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, slashRange);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }

        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            TakeDamage(1);
        }
    }
}

