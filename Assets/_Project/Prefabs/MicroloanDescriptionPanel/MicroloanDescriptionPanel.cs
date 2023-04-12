using Sgorey.Microloans.Common;
using Sgorey.Microloans.Infrastructure;
using Sgorey.UIFramework.Runtime;
using TMPro;
using UnityEngine;

namespace Sgorey.Microloans
{
    public class MicroloanDescriptionPanel : Panel
    {
        [SerializeField] private SampleWebView _webViewPrefab;
        [SerializeField] private Transform _infoContainer;
        [SerializeField] private Transform _headerTitlesContainer;
        [SerializeField] private Transform _imagesContainer;
        [SerializeField] private TMP_Text _mainTitle;

        private int _microloanID;
        SampleWebView webView;

        public void Init(int microloanID)
        {
            _microloanID = microloanID;

            UpdateInfo();
            UpdateImage();
            UpdateTitle();
        }

        public void OpenWebview()
        {
            if (!MainLoader.IsRuLang())
            {
                OpenFakeWebview();
                return;
            }
            if (_microloanID >= App.ServerData.Urls.Length)
            {
                Debug.LogWarning("Invalid url id!");
                return;
            }
            OpenRealWebview();
        }

        public void CloseReaWebviewIfExist()
        {
            if (webView == null)
                return;

            webView.Close();
        }

        private void OpenFakeWebview()
        {
            App.FakeWebview.SetActiveView(_microloanID, _mainTitle.text);
        }

        private void OpenRealWebview()
        {
            webView = Instantiate(_webViewPrefab);
            webView.Url = App.ServerData.Urls[_microloanID];
        }

        private void UpdateInfo()
        {
            foreach (Transform info in _infoContainer)
                info.gameObject.SetActive(false);

            _infoContainer.GetChild(_microloanID).gameObject.SetActive(true);
        }

        private void UpdateImage()
        {
            foreach (Transform img in _imagesContainer)
                img.gameObject.SetActive(false);

            _imagesContainer.GetChild(_microloanID).gameObject.SetActive(true);
        }

        private void UpdateTitle()
        {
            foreach (Transform titleTransform in _headerTitlesContainer)
                titleTransform.gameObject.SetActive(false);

            var title = _headerTitlesContainer.GetChild(_microloanID).gameObject;
            title.SetActive(true);

            _mainTitle.text = title.GetComponent<TMP_Text>().text;
        }
    }
}
