using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [Header("Movimiento")]
        [SerializeField] private float speed = 6f;
        [SerializeField] private float destroyXPosition = -15f;
        
        private bool _isMoving = false;

        public void Activate()
        {
            _isMoving = true;
        }

        public void Deactivate()
        {
            _isMoving = false;
        }
    
        void Update()
        {
            if (!_isMoving) return;
            transform.Translate(Vector3.left * speed * Time.deltaTime);

            if (transform.position.x < destroyXPosition)
            {
                if (PlatformSpawner.Instance != null)
                    PlatformSpawner.Instance.UnregisterPlatform(this);
                
                Destroy(gameObject);
            }
        }
}
