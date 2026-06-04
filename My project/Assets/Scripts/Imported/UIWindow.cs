using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace Vic.Code
{
    public class UIWindow : MonoBehaviour
    {
        [Header("UI Window")]
        [SerializeField] private string windowId;
 
        [Header("References")]
        [SerializeField] private Canvas      canvas;
        [SerializeField] private CanvasGroup canvasGroup;
 
        [Header("Settings")]
        [SerializeField] private bool  hideOnStart = true;
        [SerializeField] private Ease  easeIn      = Ease.OutBack;
        [SerializeField] private Ease  easeOut     = Ease.InBack;
        [SerializeField] private float duration    = 0.5f;

        public string        WindowId             => windowId;
        public float         Duration             => duration;
        public Ease          EaseIn               => easeIn;
        public Ease          EaseOut              => easeOut;
        public RectTransform RectTransformCanvas  => canvas.GetComponent<RectTransform>();
        
        private RectTransform _rt;
        public  RectTransform _rectTransformCanvasGroup => _rt;

 
        public void Awake()
        {

            _rt = canvasGroup.GetComponent<RectTransform>();
        }
 
        private void OnDestroy()
        {

            if (_rt != null)
                _rt.DOKill();
        }
 
        private void Start()
        {
            Initialize();
        }
 
        public virtual void Initialize()
        {
            canvas.gameObject.SetActive(!hideOnStart);
            _rt.localScale = Vector3.zero;
        }
 
 
        [Button]
        public virtual void Show()
        {
            if (_rt == null) return;

            _rt.DOKill();
 
            canvas.gameObject.SetActive(true);
            _rt.DOScale(Vector3.one, duration)
               .SetEase(easeIn)
               .SetUpdate(true)      
               .OnComplete(() =>
               {
                   Debug.Log($"[UIWindow] Show completo: {windowId}");
               });
        }
 
        [Button]
        public virtual void Hide()
        {
            if (_rt == null) return;
 
            _rt.DOKill();
 
            _rt.DOScale(Vector3.zero, duration)
               .SetEase(easeOut)
               .SetUpdate(true)
               .OnComplete(() =>
               {
                   if (canvas != null)
                       canvas.gameObject.SetActive(false);
               });
        }
 
        public virtual void HideImmediate()
        {
            if (_rt != null) _rt.DOKill();
            _rt.localScale = Vector3.zero;
            if (canvas != null) canvas.gameObject.SetActive(false);
        }
    }
}
