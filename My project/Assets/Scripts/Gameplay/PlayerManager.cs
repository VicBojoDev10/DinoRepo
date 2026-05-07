using System;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [Header("Movement & Jump")] 
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;

    [Header("Attack")] 
    public float slashCooldown = 2f;
    private float currentCooldown = 0f;
    public Transform slashPoint;
    public float slashRange = 1f;
    public LayerMask enemyLayer;

    [Header("Health & States")] 
    public int lives = 2;
    private Animator _animator;
    private bool isTakingDamage;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (currentCooldown > 0) currentCooldown -= Time.deltaTime;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Jump();
        }
    }

    public void OnAttackInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Attack();
        }
    }

    public void Attack()
    {
        if (currentCooldown <= 0f && !isTakingDamage)
        {
            _animator.SetTrigger("SlashCooldown");
            
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(slashPoint.position, slashRange, enemyLayer);
            bool hitSomething = false;

            foreach (Collider2D enemy in hitEnemies)
            {
                hitSomething = true;
            }
            
            currentCooldown = hitSomething ? 0f : slashCooldown;
            
            Debug.Log("SlashMade" + hitSomething);
        }
    }

    public void Jump()
    {
        if (isGrounded && !isTakingDamage)
        {
            _animator.SetTrigger("Jump");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            AchievementManager.Instance?.AddJump();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(isTakingDamage) return;

        if (other.CompareTag("Obstacle") || other.CompareTag("Enemy"))
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
            _animator.SetTrigger("Death");
        }
    }

    private IEnumerator DamageRoutine()
    {
        isTakingDamage = true;
        _animator.SetTrigger("TakeDamage");

        yield return new WaitForSeconds(3f);
        
        _animator.SetTrigger("Recover");
        isTakingDamage = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (slashPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(slashPoint.position, slashRange);
    }
}

