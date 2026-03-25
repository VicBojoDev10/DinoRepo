using NaughtyAttributes;
using UnityEngine;

public class GameManagerUI : MonoBehaviour
{
    [Button]

    public void StartingGame()
    {
        PopUpUi popopui = UiManager.Instance.GetWindow(UIWindowsIds.PopUpUI) as PopUpUi;
        if(popopui == null) return;
        popopui.AddText("On game finished");
    }
    
}
