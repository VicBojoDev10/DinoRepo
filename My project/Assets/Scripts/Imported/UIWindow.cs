using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class UIWindow : MonoBehaviour
{
    [Header("UI Window")] [SerializeField] private string windowId;
    
    [Header("UI References")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private bool hideOnStart = true;
    
    private RectTransform _rectTransformCanvasGroup => canvasGroup.GetComponent<RectTransform>();
    public string WindowId => windowId;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialize();
    }
    
    
    public void Initialize()
    { 
        canvas.gameObject.SetActive(!hideOnStart);
        _rectTransformCanvasGroup.localScale = new Vector3(0, 0, 0);
    }
 
    [Button]
    public virtual void Show()
    {
        canvas.gameObject.SetActive(true);
        _rectTransformCanvasGroup.DOScale(Vector3.one, 0.7f).OnComplete(() =>
        {
            
        });
    }
    
    [Button]
    public virtual void Hide()
    {
        _rectTransformCanvasGroup.DOScale(Vector3.zero, 0.7f).OnComplete(() =>
        {
            canvas.gameObject.SetActive(false);
        });
    }
}
