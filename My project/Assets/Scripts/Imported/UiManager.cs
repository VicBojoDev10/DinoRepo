using System.Collections.Generic;
using NaughtyAttributes;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using Vic.Code;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance { get; private set; }
    [SerializeField] private List<UIWindow> windows = new List<UIWindow>(); 

    private void Awake()
    {
        if(Instance != null &&  Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        
    }
 
    [Button]
    public void ShowPopup()
    {
        ShowWindow(UIWindowsIds.PopUpUI);

    }
    [Button]
    private void FoundUIScene()
    {
        windows.Clear();
        var uiWindows = gameObject.GetComponentsInChildren<UIWindow>(true);
        foreach (var uiWindow in uiWindows)
        {
            if (!windows.Contains(uiWindow))
            {
                windows.Add(uiWindow);
            }
        }
    }

    public void ShowWindow(string windowId)
    {
        //var uiWindow = windows.Find(x => x.name == windowId);
        UIWindow windowToShow = null;
        foreach (UIWindow window in windows)
        {
            if (window.WindowId == windowId)
            {
                windowToShow = window;
                break;
            }
        }

        if (windowToShow != null)
        {
            windowToShow.Show();
        }
        else
        {
            Debug.LogError($"No se encontro la ventana con ID {windowId}");
        }
        
        //if (windowToShow != null))
    }

    public UIWindow GetWindow(string windowId)
    {
        foreach (UIWindow window in windows)
        {
            if (window.WindowId == windowId)
            {
                return window;
            }
        }
        return null;
    }
}

public static class UIWindowsIds
{
    public const string PopUpUI = "popupui";
    public const string StoreUI = "storeui";
    public const string InventoryUI = "inventoryui";
    public const string IntroUI = "introui";
    public const string MenuUI = "menuui";
    public const string SkinsUI = "skinsui";
    public const string AchievementsUI = "achievementsui";
    public const string InformationUI = "informationui";
    public const string UpgradesUI = "upgradesui";
    public const string PauseUI = "pauseui";
    public const string ReviveUI = "reviveui";
    public const string RetryUI = "retryui";
    public const string GameplayUI = "gameplayui";

    public const string AudioUI = "audioui";
    
}
