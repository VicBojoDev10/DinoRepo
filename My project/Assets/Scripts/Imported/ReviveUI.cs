using UnityEngine;
using Vic.Code;

public class ReviveUI : UIWindow
{
    [SerializeField] private RetryUI retryUI;
    
    [SerializeField] private PlayerManager playerManager;
    
    public override void Show()
    {
        base.Show();
        Time.timeScale = 0f;
    }

    public void OnReviveSuccess()
    {
        Time.timeScale = 1f;
        this.Hide();
    }

    public void OnSkipRevive()
    {
        this.Hide();
        retryUI.Show();
    }
    
    public void OnAcceptRevive()
    {
        playerManager.OnReviveSuccess();
    }

    public void OnDeclineRevive()
    {
        playerManager.OnReviveSuccess();
    }
}
