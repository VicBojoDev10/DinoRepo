using UnityEngine;
using Vic.Code;
public class PauseUI : UIWindow
{
    [SerializeField] private MenuUI menuUI;
    
    public void OnContinueClicked()
    {
        Time.timeScale = 1f;
        this.Hide();
    }

    public void OnExitToMenu()
    {
        Time.timeScale = 1f;
        this.Hide();
        menuUI.Show();  
    }
}
