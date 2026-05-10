using UnityEngine;
using System.Collections;
using Vic.Code;

public class GameplayController : MonoBehaviour
{
    public static GameplayController Instance;
    
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Animator enemyAnimator;
    
    [SerializeField] private GameObject worldContainer;

    [SerializeField] private float timeToHideEnemy = 5f;

    private void Awake()
    {
        Instance = this;
        if(worldContainer != null) worldContainer.SetActive(true);
    }

    public void StartGameSequence()
    {
        playerController.PlayStartIntro();
        if(enemyAnimator != null) enemyAnimator.SetTrigger("Attack");

        Debug.Log("Intro Activated");
    }

    public void OnintroSequenceFinished()
    {
        playerController.SetRunning(true);
        playerController.SetPhysicsActive(true);

        StartCoroutine(HideEnemyAfterDelay());
        
        Debug.Log("gameplay Activated");
    }

    private IEnumerator HideEnemyAfterDelay()
    {
        Rigidbody2D enemyRB = enemyAnimator.GetComponent<Rigidbody2D>();
        if (enemyRB != null)
        {
            enemyRB.linearVelocity = Vector2.zero;
            enemyRB.bodyType = RigidbodyType2D.Kinematic;
        }
        yield return new WaitForSeconds(timeToHideEnemy);

        if (enemyAnimator != null)
        {
            enemyAnimator.SetTrigger("Hide");
            Debug.Log("Executing HIde");
        }
    }
}
