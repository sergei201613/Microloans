using UnityEngine;

namespace Sgorey.Microloans
{
    public class MainPanelsContainer : MonoBehaviour
    {
        [SerializeField] private BankInfoPanelContainer[] _infoContainers;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private RectTransform _rect;
        [SerializeField] private RectTransform _canvasRect;

        private void Update()
        {
            UpdateWidth();
        }

        public void Refresh()
        {
            foreach (var container in _infoContainers)
            {
                container.InvokeUpdated();
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (_canvasRect == null)
                return;

            UpdateWidth();
        }

        private void UpdateWidth()
        {
            float width = _canvasRect.rect.width * 3f;
            float height = _rect.sizeDelta.y;
            _rect.sizeDelta = new Vector2(width, height);
        }
    }
}
