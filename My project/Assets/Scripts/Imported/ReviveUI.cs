using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Vic.Code;

public class ReviveUI : UIWindow
{
   [Header("Configuración de Revivir")]
    [SerializeField] private Button reviveItemButton;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float timeToGameOver = 5f;
    
    [SerializeField] private RetryUI retryUI;
    private PlayerController playerController;

    private Coroutine _countDownCoroutine;

    public override void Initialize()
    {
        base.Initialize();
        reviveItemButton.onClick.AddListener(OnReviveItemClicked);
    }

    public override void Show()
    {
        base.Show();
        
        if (_countDownCoroutine != null) StopCoroutine(_countDownCoroutine);
        _countDownCoroutine = StartCoroutine(ReviveTimerRoutine());
    }

    private IEnumerator ReviveTimerRoutine()
    {
        float timer = timeToGameOver;

        while (timer > 0)
        {
            
            if (timerText != null) timerText.text = Mathf.CeilToInt(timer).ToString();
            
            timer -= Time.deltaTime;
            yield return null;
        }
        
        Debug.Log("Out of time, game over");
        OnTimeExpired();
    }

    public void OnReviveItemClicked()
    {
        if (_countDownCoroutine != null) StopCoroutine(_countDownCoroutine);
        
        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.ActionRevive();
        }
    }

    private void OnTimeExpired()
    {
        this.Hide();

        if (retryUI != null)
        {
            retryUI.Show();
        }
        else if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.ActionRetry();
        }
        Debug.Log("Out of time, game over");
    }
    
    public override void Hide()
    {
        base.Hide();
        if (_countDownCoroutine != null) StopCoroutine(_countDownCoroutine);
    }
}
