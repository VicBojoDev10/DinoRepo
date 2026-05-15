using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float speed = 6f;
    [SerializeField] private float destroyXPosition = -15f;

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        
        if (transform.position.x < destroyXPosition)
        {
            Destroy(gameObject);
        }
    }
}
