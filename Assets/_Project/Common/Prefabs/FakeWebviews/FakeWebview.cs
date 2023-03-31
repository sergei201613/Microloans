using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sgorey.Microloans
{
    public class FakeWebview : MonoBehaviour
    {
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Slider _priceSlider;

        private void Awake()
        {
            SetPriceText(_priceSlider.value);
        }

        private void OnEnable()
        {
            _priceSlider.onValueChanged.AddListener(SetPriceText);
        }

        private void OnDisable()
        {
            _priceSlider.onValueChanged.RemoveListener(SetPriceText);
        }

        private void SetPriceText(float value)
        {
            _priceText.text = value.ToString();
        }
    }
}
