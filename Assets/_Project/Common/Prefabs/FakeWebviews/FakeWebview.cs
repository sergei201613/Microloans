using System;
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

        private void Awake()
        {
            SetPriceText(_priceSlider.value);
        }

        private void OnEnable()
        {
            _priceSlider.onValueChanged.AddListener(SetPriceText);
            _confirmButton.onClick.AddListener(Confirm);
        }

        private void OnDisable()
        {
            _priceSlider.onValueChanged.RemoveListener(SetPriceText);
            _confirmButton.onClick.RemoveListener(Confirm);
        }

        private void Confirm()
        {
            _confirmPanel.SetActive(true);
        }

        private void SetPriceText(float value)
        {
            _priceText.text = value.ToString();
        }
    }
}
