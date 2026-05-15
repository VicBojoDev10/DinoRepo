using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    public enum EnemyType {Rocks, Worms}

    public EnemyType type;
    
    private Rigidbody2D _rb;
    private Collider2D _col;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();

        if (_col != null)
        {
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            _rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        }
    }

    public void OnHitBySlash()
    {
        if (type == EnemyType.Worms)
        {
            _rb.bodyType = RigidbodyType2D.Dynamic;
            _rb.AddForce(new Vector2(5,5) , ForceMode2D.Impulse);
            Destroy(gameObject,1.5f);
        }
        else
        {
            Debug.Log("This enemy can't be slashed");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerManager.Instance.TakeDamage(1);
        }
    }
}
