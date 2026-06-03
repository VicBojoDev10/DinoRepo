using UnityEngine;
using UnityEngine.InputSystem;
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
 
    private Rigidbody2D _rb;
    private float       _verticalVelocity;
    private bool        _physicsEnabled;
    private bool        _isGrounded;

 
    private void Awake()
    {
        Instance = this;
        _rb = GetComponent<Rigidbody2D>();
 
        _rb.bodyType       = RigidbodyType2D.Dynamic;
        _rb.gravityScale   = 1f;
        _rb.freezeRotation = true;
        _rb.constraints    = RigidbodyConstraints2D.FreezeRotation;
    }
 
    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    private void FixedUpdate()
    {
        if (!_physicsEnabled) return;
 
        _isGrounded = groundCheck != null
            ? Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer)
            : false;
 
        if (!_isGrounded || _verticalVelocity > 0f)
            _verticalVelocity -= gravity * Time.fixedDeltaTime;
        else
            _verticalVelocity = 0f;

        _rb.linearVelocity = new Vector2(0f, _verticalVelocity);
    }

 
    public void EnablePhysics()
    {
        _verticalVelocity = 1f;
        _physicsEnabled   = true;
    }
 
    public void Jump(bool isHighJump = false)
    {
        if (!_physicsEnabled) return;
 
        bool grounded = groundCheck != null
            ? Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer)
            : false;
 
        if (grounded)
            _verticalVelocity = isHighJump ? jumpForce * 1.6f : jumpForce;
    }
 
 
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("DeathZone"))
            GameplayController.Instance?.TriggerGameOver();
    }
 
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
            GameplayController.Instance?.TriggerGameOver();
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

