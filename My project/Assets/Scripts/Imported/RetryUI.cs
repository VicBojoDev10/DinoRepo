using UnityEngine;
using UnityEngine.UI;
using Vic.Code;

public class RetryUI : UIWindow
{
    [SerializeField] private Button retryButton;
    [SerializeField] private Button menuButton;

    public override void Initialize()
    {
        base.Initialize();
        if(retryButton != null)
            retryButton.onClick.AddListener(OnRetryClicked);
        if(menuButton != null)
            menuButton.onClick.AddListener(OnMenuClicked);
    }

    private void OnMenuClicked()
    {
        this.Hide();
        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.ActionBackMenu();
        }
    }

    private void OnRetryClicked()
    {
        this.Hide();
        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.ActionRetry();
        }
    }
}
