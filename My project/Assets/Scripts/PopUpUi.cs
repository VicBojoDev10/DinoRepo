using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using TMPro;


public class PopUpUi : UIWindow
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private Button _buttonNO;
    [SerializeField] private Button _buttonYES;
    
    #region UI Window
    [SerializeField] private RectTransform popUpRectTransform;
    [SerializeField] private float initialHeight;
    private float _initialY;
    private float _finalY;

    public void Initialize()
    {
        _initialY = rectTransformCanvasGroup.rect.height + (popUpRectTransform.rect.height * 2);
        _finalY = rectTransformCanvasGroup.position.y;

        popUpRectTransform.DOMoveY(_initialY, 0f);
        _buttonNO.onClick.AddListener(OnNoButtonClick);
        _buttonYES.onClick.AddListener(OnYesButtonClicked);
    }

    public override void Show()
    {
        popUpRectTransform.gameObject.SetActive(true);
        popUpRectTransform.DOMoveY(_finalY, 1.5f).OnComplete(() => {});
    }

    public override void Hide()
    {
        popUpRectTransform.DOScale(Vector3.zero, 1.5f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            popUpRectTransform.gameObject.SetActive(false);
        });
    }

    #endregion

    private void OnDestroy()
    {
        _buttonYES.onClick.RemoveListener(OnYesButtonClicked);
        _buttonNO.onClick.RemoveListener(OnNoButtonClick);
    }

    public void AddText(string text)
    {
        _titleText.text = text;
    }

    #region PopUpUI

    private void OnYesButtonClicked()
    {
        Debug.Log("Yes Activated");
    }

    private void OnNoButtonClick()
    {
        Debug.Log("No Activated");
    }

    public void SetText(string content)
    {
        _titleText.text = content;
    }
    #endregion
}
