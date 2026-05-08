using System;
using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;
using Vic.Code;

public class AchievementsUI : UIWindow
{
    
    public GameObject achievementListpanel;
    public GameObject achievementDetailPanel;
    public GameObject achievementPanel;
    public GameObject achievementName;
    public GameObject achievementText;
    public GameObject returnButton;
    public GameObject upgradesCnvas;

    public override void Initialize()
    {  
        ShowAchievementList();
        base.Initialize();
    }
    
    public void ShowAchievementList()
    {
        achievementListpanel.SetActive(true);
        achievementDetailPanel.SetActive(false);
        
        achievementName.SetActive(true);
        achievementText.SetActive(false);
        returnButton.SetActive(true);
    }
    public void HideAchievementInfo()
    {
        achievementListpanel.SetActive(false);
        achievementDetailPanel.SetActive(true);
        
        achievementName.SetActive(false);
        achievementText.SetActive(true);
        returnButton.SetActive(true);
    }

    public void OnReturnButtonCicked()
    {
        if (achievementListpanel.activeSelf)
        {
            ShowAchievementList();
        }
        else if (achievementDetailPanel.activeSelf)
        {
            achievementListpanel.SetActive(false);
            achievementDetailPanel.SetActive(true);

            if (upgradesCnvas != null)
            {
                upgradesCnvas.SetActive(true);
            }
            
            this.Hide();
        }
        
    }
}
