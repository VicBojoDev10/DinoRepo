using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private const string STATE_IDLE      = "Idle";
    private const string STATE_RUN       = "Run";
    private const string STATE_JUMP      = "Jump";
    private const string STATE_SLASH     = "Slash";
    private const string STATE_HIT       = "GetHit";
    private const string STATE_DEATH     = "Death";
    private const string STATE_REVIVE    = "Revive";
    private const string STATE_INTRO     = "StartGame";
 
    private Animator _animator;
    private bool     _isRunning;
 
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.applyRootMotion = false;
    }
 
    private void OnDestroy()
    {
        _isRunning = false;
    }
 
    public void PlayStartIntro()
    {
        _isRunning = false;
        Play(STATE_INTRO);
    }

    public void SetRunning(bool isRunning)
    {
        _isRunning = isRunning;
        Play(isRunning ? STATE_RUN : STATE_IDLE);
    }

    public void TriggerJump()
    {
        Play(STATE_JUMP);
    }
 
    public void TriggerSlash()
    {
        Play(STATE_SLASH);
    }
 
    public void TriggerDamage()
    {
        Play(STATE_HIT);
    }

    public void TriggerDeath()
    {
        _isRunning = false;
        Play(STATE_DEATH);
    }
 
    public void TriggerRevive()
    {
        Play(STATE_REVIVE);
    }

    public void ForceMenuIdle()
    {
        _isRunning = false;
        Play(STATE_IDLE);
    }

    private void Play(string stateName)
    {
        if (_animator == null) return;
        _animator.Play(stateName, 0, 0f);
    }
} 
