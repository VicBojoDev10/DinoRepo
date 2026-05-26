using UnityEngine;
using Vic.Code;
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    private Rigidbody2D _rb;

    private void Awake()
    {
        Instance = this;
        _rb = GetComponent<Rigidbody2D>();
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

