using UnityEngine;
using Vic.Code;

public class UpgradesUI : UIWindow
{
    [SerializeField] private MenuUI menuUI;
    [SerializeField] private SkinsUI skinsUI;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private AchievementsUI achievementsUI;
    
    public void OpenSkins()
    {
        {
            skinsUI.Show();
        }
    }
    public void OpenInventory()
    {
        {
            inventoryUI.Show();
        }
    }
    public void OpenAchievements()
    {
        {
            achievementsUI.Show();
        }
    }

    public void OnReturnToMenu()
    {
        this.Hide(); 
        menuUI.Show(); 
    }
}
