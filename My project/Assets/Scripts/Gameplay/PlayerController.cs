using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayStartIntro()
    {
        if (_animator != null)
        {
            _animator.SetBool("IsRunning", false);
            _animator.SetTrigger("StartGame"); // Pasa de DrakoMenuIdle a DrakoScared
        }
    }

    public void SetRunning(bool running)
    {
        if (_animator != null)
        {
            _animator.SetBool("IsRunning", running);
        }
    }

    public void TriggerJump()
    {
        if (_animator != null)
        {
            _animator.ResetTrigger("Jump");
            _animator.SetTrigger("Jump");
        }
    }

    public void TriggerSlash()
    {
        if (_animator != null)
        {
            _animator.ResetTrigger("Slash");
            _animator.SetTrigger("Slash");
        }
    }

    public void TriggerDamage()
    {
        if (_animator != null)
        {
            transform.rotation = Quaternion.identity;

            _animator.ResetTrigger("GetHit");
            _animator.SetTrigger("GetHit");
            
            _animator.Play("DrakoDamaged", 0, 0f);
        }
    }

    public void TriggerDeath()
    {
        if (_animator != null)
        {
            _animator.SetBool("IsRunning", false);
            _animator.ResetTrigger("Die");
            _animator.SetTrigger("Die");
        }
    }

    public void TriggerRevive()
    {
        if (_animator != null)
        {
            transform.rotation = Quaternion.identity;

            _animator.ResetTrigger("Revive");
            _animator.SetTrigger("Revive");
            
            _animator.Play("DrakoRevive", 0, 0f);
            _animator.SetBool("IsRunning", true);
        }
    }

    public void ForceMenuIdle()
    {
        if (_animator != null)
        {
            _animator.SetBool("IsRunning", false);
            _animator.ResetTrigger("StartGame");
            _animator.ResetTrigger("Jump");
            _animator.ResetTrigger("Slash");
            _animator.ResetTrigger("GetHit");
            _animator.ResetTrigger("Die");
            _animator.ResetTrigger("Revive");
            
            _animator.Play("DrakoMenuIdle", 0, 0f);
        }
    }
} 
