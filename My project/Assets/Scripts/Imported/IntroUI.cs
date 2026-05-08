using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;
using Vic.Code;
public class IntroUI : UIWindow
{
    public GameObject canvasIntro;

    public override void Initialize()
    {
        Show();
    }

    public override void Show()
    {
        Debug.Log("Show intro");
        canvasIntro.SetActive(true);
    }
}
