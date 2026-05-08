using UnityEngine;
using Vic.Code;

public class RetryUI : UIWindow
{
    [SerializeField] private PlayerManager _playerManager;

    public override void Initialize()
    {
        base.Initialize();
    }

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
