using Sgorey.Microloans.Infrastructure;
using Sgorey.UIFramework.Runtime;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sgorey.Microloans.Common
{
    [RequireComponent(typeof(Button))]
    public class OpenMicroloansSiteButton : MonoBehaviour
    {
        [SerializeField] private int _urlId = 0;
        
        // TODO: shouldn't be here.
        [SerializeField] private SampleWebView _webViewPrefab;
        [SerializeField] private Panel[] _fakeWebviews;
        
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OpenSite);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OpenSite);
        }

        private void OpenSite()
        {
            if (!MainLoader.IsRuLang())
            {
                OpenFakeWebview();
                return;
            }
            if (_urlId >= App.ServerData.Urls.Length)
            {
                Debug.LogWarning("Invalid url id!");
                return;
            }
            OpenRealWebview();
        }

        private void OpenFakeWebview()
        {
            App.PanelManager.OpenPanel(_fakeWebviews[_urlId]);
        }

        private void OpenRealWebview()
        {
            SampleWebView ww = Instantiate(_webViewPrefab);
            ww.Url = App.ServerData.Urls[_urlId];
        }
    }
}