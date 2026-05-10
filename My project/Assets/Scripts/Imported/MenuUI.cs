using UnityEngine;
using Vic.Code;

public class MenuUI : UIWindow
{
    [SerializeField] private UpgradesUI upgradesUI;
    [SerializeField] private GameplayUI gameplayUI;
    
    public void OnPlayButtonClicked()
    {
        this.Hide();
        if (GameplayController.Instance != null)
        {
            GameplayController.Instance.StartGameSequence();   
        }
    }

    public void OnUpgradeClicked()
    {
        upgradesUI.Show();
    }
}
