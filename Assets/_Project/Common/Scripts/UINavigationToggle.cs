using Sgorey.Microloans.Infrastructure;
using Sgorey.UIFramework.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sgorey.Microloans
{
    [RequireComponent(typeof(Toggle))]
    public class UINavigationToggle : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _activeColor;
        [SerializeField] private Panel _panelToOpen;

        private Toggle _toggle;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
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
                _panelToOpen.gameObject.SetActive(true);

                App.FakeWebview.CloseActiveView();
            }
            else
            {
                color = _defaultColor;
                _panelToOpen.gameObject.SetActive(false);
            }

            _icon.color = new Color(color.r, color.g, color.b, 1f);
            _text.color = new Color(color.r, color.g, color.b, 1f);
        }
    }
}
