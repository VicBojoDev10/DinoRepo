using UnityEngine;
using UnityEngine.InputSystem;
using Vic.Code;
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    private Rigidbody2D _rb;

    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = 18f;
    private Collider2D _playerCollider2D;
    [SerializeField] private LayerMask groundLayer = 0;
    private bool _isGrounded;
    private float _verticalVelocity;
    
    private Vector2 _movement;
    private Keyboard _keyboard;

    private void Awake()
    {
        _keyboard = Keyboard.current;
        Instance = this;
        _isGrounded = false;
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0f;
        _rb.freezeRotation = true;

        _playerCollider2D = GetComponent<Collider2D>();
    }

    void Update()
    {
        _movement = Vector2.zero;

        _isGrounded = _playerCollider2D != null && _playerCollider2D.IsTouchingLayers(groundLayer);
        
        if (_keyboard.spaceKey.wasPressedThisFrame && _isGrounded)
        {
            _verticalVelocity = jumpForce;
        }

        if (!_isGrounded || _verticalVelocity > 0)
        {
            _verticalVelocity -= gravity * Time.deltaTime;
        }
        else
        {
            _verticalVelocity = 0;
        }
        _movement.y = _verticalVelocity;
        
        _movement *= Time.deltaTime;
        
        transform.Translate(_movement);
        
    }

    public void SetPhysicsActive(bool active)
    {
        _rb.bodyType = active ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
        
        if (!active)
        {
            _rb.linearVelocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeathZone"))
        {
            Debug.Log("Jugador cayó al vacío.");
            GameplayController.Instance.TriggerGameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameplayController.Instance.TriggerGameOver();
        }
    }
}

