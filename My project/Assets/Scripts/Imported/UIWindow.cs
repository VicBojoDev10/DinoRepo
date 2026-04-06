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
        [SerializeField] private Canvas canvas;
        [SerializeField] private CanvasGroup canvasGroup;
        
        [Header("Settings")]
        [SerializeField] private bool hideOnStart = true;
        [SerializeField] private Ease easeIn = Ease.OutBack;
        [SerializeField] private Ease easeOut = Ease.InBack;
        [SerializeField] private float duration = 0.5f;

        public string WindowId => windowId;
        public RectTransform _rectTransformCanvasGroup => canvasGroup.GetComponent<RectTransform>();
        public RectTransform RectTransformCanvas => canvas.GetComponent<RectTransform>();
        public float Duration => duration;
        public Ease EaseIn => easeIn;
        public Ease EaseOut => easeOut;

        public void Awake()
        {
            
        }
        void Start()
        {
            Initialize();
        }

        public virtual void Initialize()
        {
            //Apagar el objeto del canvas al iniciar el juego
            canvas.gameObject.SetActive(!hideOnStart);
            _rectTransformCanvasGroup.localScale = Vector3.zero;
        }

         [Button]
        public virtual void Show()
        {
            Debug.Log($"Showing window: {windowId}");
            canvas.gameObject.SetActive(true);
            _rectTransformCanvasGroup.DOScale(Vector3.one, duration).SetEase(easeIn).OnComplete(() =>
            {
                Debug.Log( "Finished anim showing window: " + windowId);
            });
            
        }
        
        [Button]
        public virtual void Hide()
        {
            Debug.Log($"Hiding window: {windowId}");
            _rectTransformCanvasGroup.DOScale(Vector3.zero, duration).SetEase(easeOut).OnComplete(() =>
            {
                Debug.Log( "Finished anim hiding window: " + windowId);
                canvas.gameObject.SetActive(false);
            });
        }
}
 
}
