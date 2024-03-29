using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

namespace Sgorey.UIFramework.Runtime
{
    [RequireComponent(typeof(RectTransform))]
    public class Panel : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        [Header("Animation")]
        [SerializeField] private bool _isVerticalAnim = false;
        [Space]
        [SerializeField] private RectTransform _canvasRect;
        [SerializeField] protected RectTransform _rectTransform;

        protected float animDuration = 0.35f;

        protected float CanvasHeight => _canvasRect.rect.height;
        protected float CanvasWidth => _canvasRect.rect.width;

        private PanelManager _panelManager;

        protected virtual void Awake()
        {
        }

        protected virtual void OnEnable()
        {
            if (_backButton)
                _backButton.onClick.AddListener(Back);
        }

        protected virtual void OnDisable()
        {
            if (_backButton)
                _backButton.onClick.RemoveListener(Back);
        }

        protected virtual void Update() { }

        public virtual void Init(PanelManager panelManager)
        {
            _panelManager = panelManager;
            _canvasRect = _panelManager.GetComponent<RectTransform>();
        }

        public void Open(bool animate, Action onComplete = null)
        {
            gameObject.SetActive(true);

            if (!animate)
                return;

            if (_isVerticalAnim)
                DoVerticalAnimOpen();
            else
                DoHorizontalAnimOpen(onComplete);
        }

        public void Close(bool animate)
        {
            if (!animate)
            {
                gameObject.SetActive(false);
                return;
            }

            transform.SetAsLastSibling();

            if (_isVerticalAnim)
                DoVerticalAnimClose();
            else
                DoHorizontalAnimClose();
        }

        protected virtual void DoHorizontalAnimOpen(Action onComplete = null)
        {
            _rectTransform.anchoredPosition = new Vector2(CanvasWidth, 0f);
            _rectTransform
                .DOAnchorPosX(0f, animDuration)
                .OnComplete(() =>
                {
                    onComplete?.Invoke();
                });
        }

        protected virtual void DoHorizontalAnimClose()
        {
            _rectTransform.DOAnchorPosX(CanvasWidth, animDuration)
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                });
        }

        private void DoVerticalAnimOpen()
        {
            _rectTransform.anchoredPosition = new Vector2(0f, -CanvasHeight);
            _rectTransform.DOAnchorPosY(0f, animDuration);
        }

        private void DoVerticalAnimClose()
        {
            _rectTransform.DOAnchorPosY(-CanvasHeight, animDuration)
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                });
        }

        public void Back()
        {
            _panelManager.Back(out var _);
        }
    }
}
