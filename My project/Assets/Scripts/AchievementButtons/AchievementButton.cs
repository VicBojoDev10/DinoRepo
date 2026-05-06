using UnityEngine;
using UnityEngine.UI;

public class AchievementButton : MonoBehaviour
{
    public Button button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        AchievementsUI achievementsUI = UiManager.Instance.GetWindow(UIWindowsIds.AchievementsUI) as AchievementsUI;
        if(achievementsUI == null)
        {
            Debug.Log("Ayuda errooor"); 
            return;
        }
        achievementsUI.HideAchievementInfo();
    }
}
