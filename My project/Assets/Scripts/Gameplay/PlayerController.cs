using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Animator _animator;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    public void SetPhysicsActive(bool active)
    {
        if (_rb == null) _rb = GetComponent<Rigidbody2D>();
    
        _rb.bodyType = active ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
    
        if (!active)
        {
            _rb.linearVelocity = Vector2.zero;
        }
        else
        {
            _rb.WakeUp();
        }
    }
    
    public void PlayStartIntro() => _animator.SetTrigger("StartGame");
    public void SetRunning(bool isRunning) => _animator.SetBool("isRunning", isRunning);
    public void TriggerJump() => _animator.SetTrigger("Jump");
    public void TriggerSlash() => _animator.SetTrigger("Slash");
    public void TriggerDamage() => _animator.SetTrigger("GetHit");

    public void TriggerDeath()
    {
        _animator.SetTrigger("Die");
        SetPhysicsActive(false);
    }

    public void TriggerRevive()
    {
        _animator.SetTrigger("Revive");
        SetPhysicsActive(true);
    }

    public void ForceMenuIdle()
    {
        _animator.SetTrigger("MenuIdle");
        SetPhysicsActive(false);
    }
} 
