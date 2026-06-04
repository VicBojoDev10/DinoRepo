using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    public enum EnemyType { Rocks, Worms }
 
    [Header("Configuración")]
    public EnemyType type;
 
    [Tooltip("Fuerza del impulso al ser golpeado con slash (solo Worms).")]
    [SerializeField] private Vector2 slashKnockback = new Vector2(4f, 6f);
 
    [Tooltip("Segundos hasta destruirse tras recibir slash.")]
    [SerializeField] private float destroyDelay = 1.2f;
 
    private Rigidbody2D _rb;
    private Collider2D  _col;
    private bool        _isDead;

    private void Awake()
    {
        _rb  = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
 
        if (_rb != null)
        {
            _rb.constraints   = RigidbodyConstraints2D.FreezeAll;
            _rb.interpolation = RigidbodyInterpolation2D.Interpolate;
            _rb.bodyType      = RigidbodyType2D.Kinematic;
        }
    }

    public void OnHitBySlash()
    {
        if (_isDead) return;
 
        if (type == EnemyType.Worms)
        {
            KillEnemy();
        }
        else
        {
            Debug.Log("[EnemyEntity] Rock no puede ser eliminado con slash.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isDead) return;
 
        if (other.CompareTag("Player"))
        {
            CheckPlayerCollision();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_isDead) return;
 
        if (other.gameObject.CompareTag("Player"))
        {
            CheckPlayerCollision();
        }
    }

    private void CheckPlayerCollision()
    {
        PlayerManager pm = PlayerManager.Instance;

        if (pm != null && pm.IsSlashing && type == EnemyType.Worms)
        {
            KillEnemy();
            return;
        }
        
        HitPlayer();
    }
    
    private void HitPlayer()
    {
        PlayerManager pm = PlayerManager.Instance;
        if (pm != null)
            pm.GetComponent<PlayerController>()?.TriggerDamage();
        
        GameplayController.Instance?.TriggerGameOver();
        pm?.ResetForNewGame(); 
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
 
        Destroy(gameObject, destroyDelay);
    }
}
