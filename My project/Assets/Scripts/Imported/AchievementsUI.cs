using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;
using Vic.Code;

public class AchievementsUI : UIWindow
{
    public List<GameObject> achievement = new List<GameObject>();
    public GameObject achievementName;
    public GameObject achievementText;
    public GameObject achievementPanel;
    public GameObject returnButton;

    public void Start()
    {  
        ShowAchievementList();
    }
    public override void Show()
    {
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
    }

    public void ShowAchievementList()
    {
        achievement.Add(gameObject);
        achievementPanel.SetActive(true);
        achievementName.SetActive(true);
        achievementText.SetActive(false);
        returnButton.SetActive(false);
        if (achievement != null)
        {
            AchievementInfo();
        }
    }
    public void AchievementInfo()
    {
        achievement.RemoveAt(index: 0);
        achievementPanel.SetActive(false);
        achievementName.SetActive(false);
        achievementText.SetActive(true);
        returnButton.SetActive(true);
        if (returnButton == true)
        {
            ShowAchievementList();
        }
    }
}
