using TMPro;
using UnityEngine;

namespace Sgorey.Microloans
{
    public class BankInfoPanel : MonoBehaviour
    {
        public string Title => _titleText.text;

        [SerializeField] private TMP_Text _titleText;
    }
}
