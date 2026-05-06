using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using System;
using TMPro;
using Vic.Code;

public class PopUpUi : UIWindow
{
       
    [Header("PopupUI")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Button noButton;
    [SerializeField] private Button yesButton;
    
    #region UIWindow
    
    [SerializeField] private RectTransform popupRectTransform;

    private float _initialY;
    private float _finalY;
    #endregion


    public Button YesButton => yesButton;

    public Button NoButton => noButton;


    public override void Initialize()
    {
        #region Animation setup
        _initialY = _rectTransformCanvasGroup.rect.height + (popupRectTransform.rect.height * 2f);
        _finalY = RectTransformCanvas.anchoredPosition.y;

        RectTransformCanvas.gameObject.SetActive(false);
        popupRectTransform.DOMoveY(_initialY, 0f);
        #endregion
        
    }

    private void OnDestroy()
    {
        
    }


    public override void Show()
    {
        RectTransformCanvas.gameObject.SetActive(true);
        popupRectTransform.DOMoveY(_finalY, Duration).SetEase(EaseIn);
    }

    public override void Hide()
    {
        popupRectTransform.DOMoveY(_initialY, Duration).SetEase(EaseOut).OnComplete(() =>
        {
            RectTransformCanvas.gameObject.SetActive(false);
        });
        
    }


    #region PopupUI

    
    public void SetText(string content)
    { 
        titleText.text = content;
    }

    #endregion

}
