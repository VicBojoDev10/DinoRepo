using UnityEngine;
using Vic.Code;

public class RetryUI : UIWindow
{
    [SerializeField] private PlayerManager _playerManager;

    public void Awake()
    {
        DisplayRetry();
    }
    public void DisplayRetry()
    {
        if (_playerManager.lives == 0)
        {
            base.enabled = true;
        }
    }
}
