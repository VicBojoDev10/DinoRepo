using NaughtyAttributes;
using UnityEngine;

public class GameTestManager : MonoBehaviour
{
    [Button]
    public void OnGameStart()
    {
        InventoryStart();
    }

    private void InventoryStart()
    {
        UiManager.Instance.ShowWindow(UIWindowsIds.InventoryUI);
    }

    private void StoreStart()
    {
        UiManager.Instance.ShowWindow(UIWindowsIds.StoreUI);
    }
}
