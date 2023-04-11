using UnityEngine;
using UnityEngine.UI;

namespace Sgorey.Microloans.Common
{
    [RequireComponent(typeof(Button))]
    public class OpenMicroloanDescButton : MonoBehaviour
    {
        [SerializeField] private BankInfoPanel _bankInfoPanel;

        private Button Button
        {
            get
            {
                if (_button == null)
                    _button = GetComponent<Button>();

                return _button;
            }
        }

        private MicroloanDescriptionPanel DescPanel
        {
            get
            {
                if (_decsPanel == null)
                    _decsPanel = PersistentPanels.Instance.MicroloanDescription;

                return _decsPanel;
            }
        }
        
        private Button _button;
        private MicroloanDescriptionPanel _decsPanel;

        private void OnEnable()
        {
            Button.onClick.AddListener(OpenSite);
        }

        private void OnDisable()
        {
            Button.onClick.RemoveListener(OpenSite);
        }

        private void OpenSite()
        {
            DescPanel.Open(true);
            DescPanel.Init(_bankInfoPanel.ID);
        }
    }
}