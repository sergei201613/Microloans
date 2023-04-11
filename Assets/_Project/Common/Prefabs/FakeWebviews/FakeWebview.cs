using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sgorey.Microloans
{
    public class FakeWebview : MonoBehaviour
    {
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Slider _priceSlider;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private GameObject _confirmPanel;

        private ScrollRect _scroll;

        private void Awake()
        {
            _scroll = GetComponentInParent<ScrollRect>();
            SetPriceText(_priceSlider.value);
        }

        private void OnEnable()
        {
            _scroll.verticalNormalizedPosition = 1f;

            _priceSlider.onValueChanged.AddListener(SetPriceText);
            _confirmButton.onClick.AddListener(Confirm);
        }

        private void OnDisable()
        {
            _priceSlider.onValueChanged.RemoveListener(SetPriceText);
            _confirmButton.onClick.RemoveListener(Confirm);
        }

        public void Confirm()
        {
            _confirmPanel.SetActive(true);
        }

        private void SetPriceText(float value)
        {
            _priceText.text = value.ToString();
        }
    }
}
