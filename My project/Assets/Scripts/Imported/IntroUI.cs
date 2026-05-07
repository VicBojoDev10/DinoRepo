using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;
using Vic.Code;
public class IntroUI : UIWindow
{
    public GameObject canvasIntro;
    [Button]
    public override void Initialize()
    {
        canvasIntro.SetActive(true);
        base.Initialize();
    }
}
