using UnityEngine;
using System.Collections;
using Vic.Code;

public class GameplayController : MonoBehaviour
{
    public static GameplayController Instance;
    
    [SerializeField] private PlayerController player;
    [SerializeField] private Animator introEnemyAnimator;
    
    [Header("Piso Inicial de Escena")]
    [SerializeField] private GameObject initialFloor; 

    private void Awake()
    {
        Instance = this;
        
        if (initialFloor != null) initialFloor.SetActive(true);
        
        player.SetPhysicsActive(false); 
    }

    public void StartGameSequence()
    {
        player.PlayStartIntro();
        if (introEnemyAnimator != null) introEnemyAnimator.SetTrigger("Attack");
    }
    
    public void OnIntroAnimationFinished()
    {
        Debug.Log("Intro terminada: Activando físicas y spawners.");
        
        player.SetPhysicsActive(true); 
        
        player.SetRunning(true);       
        
        if (PlatformSpawner.Instance != null)
        {
            PlatformSpawner.Instance.StartSpawning();
        }
    }
}