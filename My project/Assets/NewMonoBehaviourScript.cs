
/*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTakingDamage) return;

        // Las rocas y los gusanitos deben tener IsTrigger activado
        if (collision.CompareTag("Obstacle") || collision.CompareTag("Enemy"))
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        lives--;
        if (lives > 0)
        {
            StartCoroutine(DamageRoutine());
        }
        else
        {
            animator.SetTrigger("Death");
            // Lógica de Game Over
        }
    }

    private IEnumerator DamageRoutine()
    {
        isTakingDamage = true;
        // La animación "TakeDamage" debe desplazar al jugador a la derecha
        animator.SetTrigger("TakeDamage"); 
        
        // Espera los segundos de penalización
        yield return new WaitForSeconds(3f);
        
        // La animación "Recover" lo regresa a la posición original
        animator.SetTrigger("Recover"); 
        isTakingDamage = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}



using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance { get; private set; }

    // Progreso de estadísticas
    private int totalJumps = 0;
    
    // Aquí puedes tener tu array de ScriptableObjects de logros si lo necesitas
    // public AchievementSO[] achievements;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // Cargar progreso previo (opcional)
        totalJumps = PlayerPrefs.GetInt("TotalJumps", 0);
    }

    public void AddJump()
    {
        totalJumps++;
        PlayerPrefs.SetInt("TotalJumps", totalJumps);

        CheckJumpAchievements();
    }

    private void CheckJumpAchievements()
    {
        // Lógica de verificación
        if (totalJumps >= 50 && PlayerPrefs.GetInt("Ach_Jumper1", 0) == 0)
        {
            UnlockAchievement("Ach_Jumper1", "¡Canguro principiante!");
        }
    }

    public void UnlockAchievement(string id, string name)
    {
        PlayerPrefs.SetInt(id, 1); // Marcar como desbloqueado
        Debug.Log($"Logro Desbloqueado: {name}");
        // Aquí puedes disparar un evento para que la UI muestre un popup
    }
}

using UnityEngine;
using Vic.Code;

public class AchievementsUI : UIWindow
{
    [Header("UI Panels")]
    public GameObject achievementListPanel; // El contenedor de todos los botones de logros
    public GameObject achievementDetailPanel; // El panel que muestra info de un logro específico

    [Header("Detail Elements")]
    public GameObject achievementName;
    public GameObject achievementText;
    public GameObject returnButton;

    public void Start()
    {  
        ShowAchievementList(); // Por defecto muestra la lista al iniciar[cite: 1]
    }

    // Muestra la vista general de logros
    public void ShowAchievementList()
    {
        // Encendemos la lista y apagamos los detalles
        achievementListPanel.SetActive(true);
        achievementDetailPanel.SetActive(false);
        
        // Apagamos los elementos individuales por limpieza, aunque apagar el panel padre basta[cite: 1]
        achievementName.SetActive(true);
        achievementText.SetActive(false);
        returnButton.SetActive(false);[cite: 1]
    }

    // Se llama cuando el jugador hace clic en un logro específico
    public void ShowAchievementInfo() // Renombrado de AchievementInfo para mayor claridad
    {
        // Apagamos la lista y encendemos la vista de detalles
        achievementListPanel.SetActive(false);
        achievementDetailPanel.SetActive(true);

        achievementName.SetActive(false);[cite: 1]
        achievementText.SetActive(true);[cite: 1]
        returnButton.SetActive(true);[cite: 1]
    }

    // El botón "Return" en la UI debe llamar a este método
    public void OnReturnButtonClicked()
    {
        ShowAchievementList();
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem; // Importante añadir esta librería

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [Header("Movement & Jump")]
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;

    [Header("Attack")]
    public float scratchCooldown = 2f;
    private float currentCooldown = 0f;
    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask enemyLayer;

    [Header("Health & States")]
    public int lives = 2;
    private Animator animator;
    private bool isTakingDamage = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (currentCooldown > 0) currentCooldown -= Time.deltaTime;
        
        // Comprobación de suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    // --- NUEVOS MÉTODOS PARA EL INPUT SYSTEM ---

    // Este método lo conectas a tu evento "Jump" en el componente PlayerInput
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        // context.performed asegura que solo salte al presionar, no al soltar el botón
        if (context.performed) 
        {
            Jump();
        }
    }

    // Este método lo conectas a tu evento "Attack" en el componente PlayerInput
    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Attack();
        }
    }

    // --- MÉTODOS PÚBLICOS (Se pueden llamar desde OnClick de la UI también) ---

    public void Jump()
    {
        if (isGrounded && !isTakingDamage)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            AchievementManager.Instance?.AddJump();
        }
    }

    public void Attack()
    {
        if (currentCooldown <= 0f && !isTakingDamage)
        {
            animator.SetTrigger("Scratch");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
            bool hitSomething = false;

            foreach (Collider2D enemy in hitEnemies)
            {
                hitSomething = true;
                // Destruir enemigo o aplicar lógica
            }

            // Reseteo de cooldown según el diseño de tu gameplay loop
            currentCooldown = hitSomething ? 0f : scratchCooldown; 
        }
    }

    // (Aquí iría el resto del código de TakeDamage y las corrutinas que ya hicimos)
}
 */
