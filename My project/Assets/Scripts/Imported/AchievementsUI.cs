using System;
using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;
using Vic.Code;

public class AchievementsUI : UIWindow
{
    [SerializeField] private GameObject parentButtons;
    public List<GameObject> achievement = new List<GameObject>();
    public GameObject achievementName;
    public GameObject achievementText;
    public GameObject achievementPanel;
    public GameObject returnButton;

    public override void Initialize()
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
        Debug.Log("Showing Achievements");
        achievementPanel.SetActive(false);
        achievementName.SetActive(true);
        achievementText.SetActive(false);
        returnButton.SetActive(false);
    }
    public void HideAchievementInfo()
    {
        Debug.Log("Hiding Achievements");
        achievementPanel.SetActive(false);
        achievementName.SetActive(false);
        achievementText.SetActive(true);
        returnButton.SetActive(true);
    }
}
