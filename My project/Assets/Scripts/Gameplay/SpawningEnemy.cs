using UnityEngine;

public class SpawningEnemy : MonoBehaviour
{
    [Header("Físicas de Muerte")]
    [Tooltip("Fuerza del impulso al ser golpeado con slash.")]
    [SerializeField] private Vector2 slashKnockback = new Vector2(4f, 6f);
 
    [Tooltip("Segundos hasta destruirse tras recibir el rasguño.")]
    [SerializeField] private float destroyDelay = 1.2f;

    [Header("Tiempos del Flujo Visual")]
    [Tooltip("Tiempo en segundos que corre antes de decidir ocultarse solo.")]
    [SerializeField] private float timeBeforeHide = 4f;
    [Tooltip("Duración de la animación de ocultarse (Hide) antes de destruir el objeto.")]
    [SerializeField] private float hideAnimationDuration = 0.5f;
 
    private Rigidbody2D _rb;
    private Collider2D  _col;
    private Animator    _animator;
    private bool        _isDead;

    private void Awake()
    {
        _rb       = GetComponent<Rigidbody2D>();
        _col      = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();

        if (_rb != null)
        {
            _rb.constraints   = RigidbodyConstraints2D.FreezeAll;
            _rb.interpolation = RigidbodyInterpolation2D.Interpolate;
            _rb.bodyType      = RigidbodyType2D.Kinematic;
        }

        if (_col != null)
        {
            _col.isTrigger = true;
        }
    }

    private void Start()
    {
        TriggerSpawnSequence();
    }

    private void TriggerSpawnSequence()
    {
        if (_animator != null)
        {
            _animator.SetTrigger("StartGame");    
            _animator.SetBool("IsRunning", true); 
        }
        
        Invoke(nameof(TriggerHideSequence), timeBeforeHide);
    }

    private void TriggerHideSequence()
    {
        if (_isDead) return;
        _isDead = true;

        if (_col != null) _col.enabled = false; 

        if (_animator != null)
        {
            _animator.SetBool("IsRunning", false);
            _animator.SetTrigger("Hide"); 
        }

        Destroy(gameObject, hideAnimationDuration);
    }

    public void OnHitBySlash()
    {
        if (_isDead) return;
        
        CancelInvoke(nameof(TriggerHideSequence)); 
        KillEnemy();
    }
 
    private void KillEnemy()
    {
        _isDead = true;
        
        if (_col != null) _col.enabled = false; 
        
        transform.SetParent(null);

        if (_rb != null)
        {
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            _rb.bodyType    = RigidbodyType2D.Dynamic;
            _rb.gravityScale = 2f;
            _rb.AddForce(slashKnockback, ForceMode2D.Impulse);
        }

        if (_animator != null)
        {
            _animator.SetBool("IsRunning", false);
            _animator.enabled = false; 
        }
 
        Destroy(gameObject, destroyDelay);
    }
}
