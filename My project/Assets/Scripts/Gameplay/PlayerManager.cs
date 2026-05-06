using System;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

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

    public void OnAttackInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Attack();
        }
    }

    public void Jump()
    {
        if (isGrounded && !isTakingDamage)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
           // AchievementManager.Instance?.AddJump();
        }
    }
    private void Attack()
    {
        if (currentCooldown <= 0f && !isTakingDamage)
        {
            _animator.SetTrigger("Slash");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(slashPoint.position, slashRange, enemyLayer);
            bool hitSomething = false;

            foreach (Collider2D enemy in hitEnemies)
            {
                hitSomething = true;
            }

            currentCooldown = hitSomething ? 0f : slashCooldown;
        }
    }
}

