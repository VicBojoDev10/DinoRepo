using UnityEngine;
using Vic.Code;

public class InventoryUI : UIWindow
{
    public GameObject InventoryListPanel;
    public GameObject InventoryDetailsPanel;
    public GameObject backToUpgradesButton;
    public GameObject backToListButton;
    public UIWindow upgradesUI;

    public override void Initialize()
    {
        base.Initialize();
        ShowInventoryList();
    }

    private void ShowInventoryList()
    {
        InventoryListPanel.SetActive(true);
        InventoryDetailsPanel.SetActive(false);
        
        backToUpgradesButton.SetActive(true);
        backToListButton.SetActive(false);
    }

    public void ShowInventoryDetails()
    {
        InventoryListPanel.SetActive(false);
        InventoryDetailsPanel.SetActive(true);
        
        backToUpgradesButton.SetActive(false);
        backToListButton.SetActive(true);
    }

    public void OnReturnButtonClicked()
    {
        this.Hide();
        if (upgradesUI != null)
        {
            upgradesUI.Show();
        }
    }
    public void OnBackToInventoryClicked()
    {
        ShowInventoryList();
    }
}
