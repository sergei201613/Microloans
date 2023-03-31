using Sgorey.Microloans.Infrastructure;
using UnityEngine;

namespace Sgorey.Microloans.Common
{
    public class FakeWebviewsCanvas : MonoBehaviour
    {
        [SerializeField] private GameObject[] _views;
        [SerializeField] private GameObject _container;

        private GameObject _view;

        private void Awake()
        {
            App.FakeWebview = this;
        }

        internal void SetActiveView(int viewID)
        {
            _container.SetActive(true);

            if (_view != null)
                _view.SetActive(false);

            _view = _views[viewID];
            _view.SetActive(true);
        }

        internal void CloseActiveView()
        {
            _container.SetActive(false);
        }
    }
}