using Sgorey.UIFramework.Runtime;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace Sgorey.Microloans
{
    public class CalculatorPanel : Panel
    {
        [SerializeField] private TMP_InputField _loanAmountField;
        [SerializeField] private TMP_InputField _daysField;
        [SerializeField] private TMP_InputField _rateField;
        [Space]
        [SerializeField] private TMP_Text _loanAmountText;
        [SerializeField] private TMP_Text _interestSumText;
        [SerializeField] private TMP_Text _returningText;

        private void OnEnable()
        {
            _loanAmountField.onValueChanged.AddListener(Calculate);
            _daysField.onValueChanged.AddListener(Calculate);
            _rateField.onValueChanged.AddListener(Calculate);
        }

        private void OnDisable()
        {
            _loanAmountField.onValueChanged.RemoveListener(Calculate);
            _daysField.onValueChanged.RemoveListener(Calculate);
            _rateField.onValueChanged.RemoveListener(Calculate);
        }

        private void Calculate(string text)
        {
            try
            {
                decimal amount = int.Parse(_loanAmountField.text);
                decimal days = int.Parse(_daysField.text);
                decimal rate = int.Parse(_rateField.text);

                decimal interestSum = amount / 100m * rate * days;

                _loanAmountText.text = FormatMoney(amount);
                _interestSumText.text = $"{FormatMoney(interestSum)}";
                _returningText.text = $"{FormatMoney(amount + interestSum)}";
            }
            catch
            {
                return;
            }
        }

        private string FormatMoney(decimal amount)
        {
            return amount.ToString("C", CultureInfo.CurrentCulture);
        }
    }
}
