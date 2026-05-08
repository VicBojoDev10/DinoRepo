using UnityEngine;
using Vic.Code;

public class MenuUI : UIWindow
{
    [SerializeField] private UpgradesUI upgradesUI;
    [SerializeField] private GameplayUI gameplayUI;

    public override void Initialize()
    {
        base.Initialize();
    }
    public void OnPlayerClicked()
    {
        this.Hide();
        if (GameplayController.Instance != null)
        {
            GameplayController.Instance.StartIntroCinematic();   
        }
    }

    public void OnUpgradeClicked()
    {
        upgradesUI.Show();
    }
}
