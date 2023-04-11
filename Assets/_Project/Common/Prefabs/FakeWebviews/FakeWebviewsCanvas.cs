using Sgorey.Microloans.Infrastructure;
using Sgorey.UIFramework.Runtime;
using TMPro;
using UnityEngine;

namespace Sgorey.Microloans.Common
{
    public class FakeWebviewsCanvas : MonoBehaviour
    {
        [SerializeField] private GameObject[] _views;
        [SerializeField] private GameObject _container;
        [SerializeField] private TMP_Text _titleText;

        private GameObject _view;

        private void Awake()
        {
            App.FakeWebview = this;
        }

        public void SetActiveView(int viewID, string title)
        {
            _titleText.text = title;

            _container.GetComponent<Panel>().Open(true);

            if (_view != null)
                _view.SetActive(false);

            _view = _views[viewID];
            _view.SetActive(true);
        }

        public void CloseActiveView()
        {
            _container.GetComponent<Panel>().Close(true);
        }
    }
}