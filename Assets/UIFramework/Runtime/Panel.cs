using UnityEngine;
using DG.Tweening;
using System.Collections;

namespace Sgorey.UIFramework.Runtime
{
    public class Panel : MonoBehaviour
    {
        protected RectTransform RectTransform
        {
            get
            {
                if (_rectTransform == null)
                    _rectTransform = GetComponent<RectTransform>();

                return _rectTransform;
            }
        }

        protected RectTransform CanvasRect
        {
            get
            {
                if (_canvasRect == null)
                    _canvasRect = GetComponentInParent<RectTransform>();
                
                return _canvasRect;
            }
        }

        protected float animDuration = 0.35f;

        protected float CanvasHeight => CanvasRect.rect.height;
        protected float CanvasWidth => CanvasRect.rect.width;

        private PanelManager _panelManager;
        private RectTransform _canvasRect;
        private RectTransform _rectTransform;
        private bool _isHiding;

        //private void Update()
        //{
        //    var rect = RectTransform.rect;
        //    rect.width = CanvasWidth;
        //    _rectTransform.rect.Set(rect.x, rect.y, rect.width, rect.height);
        //}

        public virtual void Init(PanelManager panelManager)
        {
            _panelManager = panelManager;
        }

        public void Show()
        {
            _isHiding = false;
            DoHorizontalAnimOpen(-1);

            gameObject.SetActive(true);
            transform.SetAsLastSibling();
        }

        private void DoHorizontalAnimOpen(int dir)
        {
            RectTransform.anchoredPosition = new Vector2(CanvasWidth * dir, 0f);
            RectTransform.DOAnchorPosX(0f, animDuration);
        }

        public void Hide()
        {
            if (!gameObject.activeInHierarchy)
                return;

            if (_isHiding)
                return;

            StartCoroutine(Coroutine());
            _isHiding = true;

            IEnumerator Coroutine()
            {
                yield return new WaitForSeconds(2f);

                if (_isHiding)
                    gameObject.SetActive(false);

                _isHiding = false;
            }
        }

        public void Back()
        {
            _panelManager.Back(out var _);
        }
    }
}
