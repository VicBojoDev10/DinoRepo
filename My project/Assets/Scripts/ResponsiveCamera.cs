using UnityEngine;
using System;
using DG.Tweening;

public class ResponsiveCamera : MonoBehaviour
{
    [SerializeField] private Vector3 cameraTabletPosition;
    [SerializeField] private Vector3 cameraMobilePosition;
    
    public static ResponsiveCamera Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdatecameraPosition();
        ResponsiveManager.Instance.OnScreenSizeChanged.AddListener(UpdatecameraPosition);
    }

    private void UpdatecameraPosition()
    {
        if (ResponsiveManager.Instance.CurrentDeviceType == DeviceType.Tablet)
        {
            gameObject.transform.position = cameraTabletPosition;
        }
        else
        {
            transform.position = cameraMobilePosition;
        }
        
    }

    public void DoImpactShake()
    {
        transform.DOShakePosition(0.5f, 0.7f, 10).SetUpdate(true).OnComplete(() =>
        {
            UpdatecameraPosition();
        });
    }
}
