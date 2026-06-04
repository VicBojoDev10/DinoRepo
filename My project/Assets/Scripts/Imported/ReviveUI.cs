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
    
    private Coroutine _countDownCoroutine;

    public override void Initialize()
    {
        base.Initialize();
        if (reviveItemButton != null)
        {
            reviveItemButton.onClick.AddListener(OnReviveItemClicked);
        }
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
            if (timerText != null) 
            {
                timerText.text = Mathf.CeilToInt(timer).ToString();
            }
            
            timer -= Time.unscaledDeltaTime; 
            yield return null;
        }
        
        OnTimeExpired();
    }

    public void OnReviveItemClicked()
    {
        if (_countDownCoroutine != null) StopCoroutine(_countDownCoroutine);

        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.ActionRevive();
            this.Hide();
        }
    }

    private void OnTimeExpired()
    {
        this.Hide();

        Time.timeScale = 1f;

        GameplayController.Instance?.TriggerGameOver();

        if (retryUI != null)
        {
            retryUI.Show();
        }
        else if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.ResetForNewGame();
        }
    }

    public override void Hide()
    {
        base.Hide();
        if (_countDownCoroutine != null) StopCoroutine(_countDownCoroutine);
    }

    private void OnRetryClicked()
    {
        this.Hide();
            
        Time.timeScale = 1f;

        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.ResetForNewGame();
        }
    }
    
}
