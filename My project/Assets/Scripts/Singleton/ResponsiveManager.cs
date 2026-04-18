using UnityEngine;
using UnityEngine.Events;
using System;
public class ResponsiveManager : MonoBehaviour
{
    public static ResponsiveManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null &&  Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    #region private Fields

    private Vector2 _lastScreenSize;

    #endregion

    #region public Properties

    public ScreenOrientation CurrentOrientation => GetScreenOrientation();
    public DeviceType CurrentDeviceType { get => GetDeviceTypeByResolution(Screen.width, Screen.height); }
    public bool IsPortrait() => Screen.width < Screen.height;
    public bool IsLandscape() => Screen.width >= Screen.height;
    public Vector2 currentScreenSize => new Vector2(Screen.width, Screen.height);
    public UnityEvent OnScreenSizeChanged { get; private set; } = new UnityEvent();
    #endregion
    
    #region Unity Methods

    private void OnEnable()
    {
        _lastScreenSize = new Vector2(Screen.width, Screen.height);
        Application.onBeforeRender += CheckScreenSizeChange;
    }


    private void OnDisable()
    {
        Application.onBeforeRender -= CheckScreenSizeChange;
    }

    private void Start()
    {
        Debug.Log(currentScreenSize);
        Debug.Log(CurrentOrientation);
        Debug.Log(CurrentDeviceType);
    }
    #endregion
    
    #region Private Methods
    private void CheckScreenSizeChange()
    {
        Vector2 currentScreenSize = new Vector2(Screen.width, Screen.height);
        if (_lastScreenSize != currentScreenSize)
        {
            _lastScreenSize = currentScreenSize;
            OnScreenSizeChanged.Invoke();
            Debug.Log($"Screen size changed: {currentScreenSize.x}x{currentScreenSize.y} Orientation: {(IsPortrait() ? "Portrait" : "Landscape")}");
            Debug.Log($"Device type: {CurrentDeviceType}");
        }
    }

    private DeviceType GetDeviceTypeByResolution(int width, int height)
    {
        float aspectRatio = (float)Math.Max(width, height) / Math.Min(width, height);
        int minDimension = Math.Min(width, height);

        if (minDimension >= 600 && aspectRatio < 2.0f)
            return DeviceType.Tablet;
        else
            return DeviceType.Mobile;
    }

    private ScreenOrientation GetScreenOrientation()
    {
        return IsPortrait() ? ScreenOrientation.Portrait : ScreenOrientation.Landscape;
    }

    #endregion

}
    public enum ScreenOrientation
    {
        Portrait,
        Landscape
    }

    public enum DeviceType
    {
        Mobile,
        Tablet
    }    
