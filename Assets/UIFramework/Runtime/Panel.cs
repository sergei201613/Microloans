using UnityEngine;
using DG.Tweening;

namespace Sgorey.UIFramework.Runtime
{
    public class Panel : MonoBehaviour
    {
        private PanelManager _panelManager;

        public virtual void Init(PanelManager panelManager)
        {
            _panelManager = panelManager;
        }

        public void Show()
        {
            //rectTransform.anchoredPosition = new Vector2(CanvasWidth, 0f);
            //rectTransform.DOAnchorPosX(0f, animDuration);

            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Back()
        {
            _panelManager.Back(out var _);
        }
    }
}
