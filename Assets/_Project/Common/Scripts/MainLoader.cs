using System.Globalization;
using Sgorey.Microloans.Infrastructure;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Sgorey.Microloans.Common
{
    public class MainLoader : MonoBehaviour
    {
        private const string IsRUKey = "RU";

        [SerializeField]
        private GameObject _loadingScreen;
        
        [SerializeField]
        private bool _shouldCheckLang = true;
        
        [SerializeField]
        private AssetReference _serverData;

        public static bool IsRuLang()
        {
            return false;
            return PlayerPrefs.GetInt(IsRUKey) == 1;
        }

        private void Awake()
        {
            if (_shouldCheckLang)
            {
                if (NeedUpdateLangInfo())
                    UpateLanguageInfo();
            }

            if (IsRuLang())
            {
                AsyncOperationHandle handle = _serverData.LoadAssetAsync<GameObject>();
                handle.Completed += Handle_Completed;

                _loadingScreen.SetActive(true);
            }
        }

        private static bool NeedUpdateLangInfo()
        {
            return !PlayerPrefs.HasKey(IsRUKey);
        }

        private void Handle_Completed(AsyncOperationHandle obj)
        {
            _loadingScreen.SetActive(false);
            
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject dataObj = Instantiate(_serverData.Asset, transform) as GameObject;
                
                if (dataObj == null)
                    return;
                
                var data = dataObj.GetComponent<ServerData>();
                
                // TODO: shit code.
                App.ServerData = data;
            }
            else
            {
                Debug.LogError($"AssetReference {_serverData.RuntimeKey} failed to load.");
            }
        }

        private void UpateLanguageInfo()
        {
            if (Application.systemLanguage != SystemLanguage.Russian)
            {
                PlayerPrefs.SetInt(IsRUKey, 0);
            }

            if (CultureInfo.CurrentCulture.ToString() != "ru-RU")
            {
                PlayerPrefs.SetInt(IsRUKey, 0);
            }

            PlayerPrefs.SetInt(IsRUKey, 1);
        }
    }
}
