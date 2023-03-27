using System;
using TMPro;
using UnityEngine;

namespace Sgorey.Microloans
{
    public class SearchPanel : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Transform _content;

        private void OnEnable()
        {
            _inputField.onValueChanged.AddListener(Filter);
        }

        private void OnDisable()
        {
            _inputField.onValueChanged.RemoveListener(Filter);
        }

        private void Filter(string title)
        {
            foreach (Transform t in _content.transform)
            {
                if (t.TryGetComponent<BankInfoPanel>(out var bankInfoPanel))
                {
                    title = title.Trim().ToLower();
                    bool active = bankInfoPanel.Title.Trim().ToLower().Contains(title);
                    t.gameObject.SetActive(active);
                }
            }
        }
    }
}
