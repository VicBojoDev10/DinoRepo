using UnityEngine;
using DG.Tweening;
using Vic.Code;

public class GameplayController : MonoBehaviour
{
 public static GameplayController Instance;

    [Header("Contenedores")]
    [SerializeField] private GameObject gameplayContainer;

    [Header("Actores de la Cinemática")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform cinematicEnemyTransform;

    [Header("Puntos de Animación")]
    public Vector3 playerLeftPos;
    public Vector3 playerStartPos;
    public Vector3 enemyApproachPos;

    [Header("UI")]
    [SerializeField] private GameplayUI gameplayUI;

    private void Awake()
    {
        Instance = this;
        
        gameplayContainer.SetActive(true);
        gameplayContainer.transform.localScale = Vector3.zero;

    }

    public void StartIntroCinematic()
    {

        Sequence introSeq = DOTween.Sequence();
        
        introSeq.Append(cinematicEnemyTransform.DOMove(enemyApproachPos, 1.2f).SetEase(Ease.InBack));
        introSeq.Join(playerTransform.DOMove(playerLeftPos, 1.5f).SetEase(Ease.OutCubic));

        introSeq.AppendInterval(0.3f);
        
        introSeq.Append(playerTransform.DOMove(playerStartPos, 0.6f).SetEase(Ease.OutBack));
        introSeq.AppendCallback(() => {
            if(ResponsiveCamera.Instance != null) ResponsiveCamera.Instance.DoImpactShake();
        });

        introSeq.Join(cinematicEnemyTransform.DOMove(cinematicEnemyTransform.position + Vector3.right * 2f, 0.5f));
        introSeq.OnComplete(() => {
            StartActualGameplay();
        });
    }

    private void StartActualGameplay()
    {
        
        gameplayContainer.transform.localScale = Vector3.one;
        
        
        if(gameplayUI != null) gameplayUI.Show();
        

        Debug.Log("Gameplay iniciado sin pausar el motor.");
    }
}

