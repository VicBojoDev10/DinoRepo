using NaughtyAttributes;
using UnityEngine;

public class GameManagerUI : MonoBehaviour
{
    [Button]

    public void StartingGame()
    {
        ShowPopUp();
    }

    public void OnGameFinished()
    {
        PopUpUi popUpUi = UiManager.Instance.GetWindow(UIWindowsIds.PopUpUI) as PopUpUi;
        if(popUpUi == null) return;
        popUpUi.SetText("On game finished");
    }
    private void ShowPopUp()
    {
        UiManager.Instance.ShowWindow(UIWindowsIds.PopUpUI);
        PopUpUi popopui = UiManager.Instance.GetWindow(UIWindowsIds.PopUpUI) as PopUpUi;
        if(popopui == null) return;
        popopui.SetText("HI");
    }

    private void HidePopUp()
    {
        UiManager.Instance.ShowWindow(UIWindowsIds.PopUpUI);
    }

    private void DoYesDebug()
    {
        Debug.Log("YES DEBUG");
    }

    private void DoNoDebug()
    {
        Debug.Log("NO DEBUG");
    }
}
