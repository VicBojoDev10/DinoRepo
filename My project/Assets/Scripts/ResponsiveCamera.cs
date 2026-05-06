using UnityEngine;
using System;

public class ResponsiveCamera : MonoBehaviour
{
    [SerializeField] private Vector3 cameraTabletPosition;
    [SerializeField] private Vector3 cameraMobilePosition;
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
        
    }
}
