using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Sgorey.Microloans
{
    [RequireComponent(typeof(Toggle))]
    public class UINavigationToggle : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _activeColor;
        [SerializeField] private MainPanelsContainer _mainPanelsContainer;
        [SerializeField] private RectTransform _panelContainer;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private int _panelPosition = 0;

        private Toggle _toggle;
        private RectTransform _canvasRect;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
            _canvasRect = _canvas.GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(bool value)
        {
            Color color;

            if (value)
            {
                color = _activeColor;

                float width = _canvasRect.rect.width;
                _panelContainer.DOAnchorPosX(-_panelPosition * width, 0.35f);
                _mainPanelsContainer.Refresh();
            }
            else
            {
                color = _defaultColor;
            }

            _icon.color = new Color(color.r, color.g, color.b, 1f);
            _text.color = new Color(color.r, color.g, color.b, 1f);
        }
    }
}
