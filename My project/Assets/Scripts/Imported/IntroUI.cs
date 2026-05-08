using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;
using UnityEditor;
using Vic.Code;
public class IntroUI : UIWindow
{
    [SerializeField] private MenuUI menuUI;

    public override void Initialize()
    {
       base.Initialize();
       base.Show();
       Invoke(nameof(GoToMenu),3f);
    }

    public void GoToMenu()
    {
        base.Hide();
        if(menuUI != null)
        {
            menuUI.Show();
        }
    }
}
