using UnityEngine;
using Vic.Code;

public class AchievementsUI : UIWindow
{

    public GameObject achievementListPanel;
    public GameObject achievementDetailPanel;
    public GameObject backToUpgradesButton;
    public GameObject backToListButton;

    public UIWindow upgradesUI;

    public override void Initialize()
    {  
        base.Initialize();
        ShowAchievementList();
    }

    public void ShowAchievementList()
    {
        achievementListPanel.SetActive(true);
        achievementDetailPanel.SetActive(false);
        
        backToUpgradesButton.SetActive(true);
        backToListButton.SetActive(false);
    }
    public void ShowAchievementDetail()
    {
        achievementListPanel.SetActive(false);
        achievementDetailPanel.SetActive(true);
        backToUpgradesButton.SetActive(false);
        backToListButton.SetActive(true);
    }

    public void OnBackToUpgradesClicked()
    {
        this.Hide();
        
        if(upgradesUI != null)
        {
            upgradesUI.Show();
        }
    }

    public void OnBackToListClicked()
    {
        ShowAchievementList();
    }
}
