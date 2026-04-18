using UnityEngine;
using NaughtyAttributes;

public class ResponsiveElement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private RectTransform rectTransform;

    [Header("Mobile Anchors")]
    [SerializeField] private Vector2 mobileAnchorMin = new Vector2(0, 0);
    [SerializeField] private Vector2 mobileAnchorMax = new Vector2(0, 0);
    
    [Header("Tablet Anchors")]
    [SerializeField] private Vector2 tabletAnchorsMin = new Vector2(0, 0);
    [SerializeField] private Vector2 tabletAnchorsMax = new Vector2(0, 0);
    
    ResponsiveManager _responsiveManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _responsiveManager = ResponsiveManager.Instance;
        _responsiveManager.OnScreenSizeChanged.AddListener(UpdateAnchors);
        UpdateAnchors();
    }

    private void UpdateAnchors()
    {
        if (_responsiveManager == null) return;

        if (_responsiveManager.CurrentDeviceType == DeviceType.Mobile)
        {
            SetMobileAnchors();
        }
        else if (_responsiveManager.CurrentDeviceType == DeviceType.Tablet)
        {
            SetTabletAnchors();
        }
    }

    private void SetTabletAnchors()
    {
        rectTransform.anchorMin = tabletAnchorsMin;
        rectTransform.anchorMax = tabletAnchorsMax;
    }

    private void SetMobileAnchors()
    {
        rectTransform.anchorMin = mobileAnchorMin;
        rectTransform.anchorMax = mobileAnchorMax;
    }

    [Button]
    private void SaveMobileAnchors()
    {
        Vector2 maxAnchors = rectTransform.anchorMax;
        Vector2 minAnchors = rectTransform.anchorMin;

        mobileAnchorMax = maxAnchors;
        mobileAnchorMin = minAnchors;
    }
    [Button]
    private void SaveTabletAnchors()
    {
        Vector2 maxAnchors = rectTransform.anchorMax;
        Vector2 minAnchors = rectTransform.anchorMin;
        
        tabletAnchorsMax = maxAnchors;
        tabletAnchorsMin = minAnchors;
    }

}
