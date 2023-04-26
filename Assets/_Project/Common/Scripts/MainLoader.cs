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

        private void Awake()
        {
            if (_shouldCheckLang)
            {
                if (NeedUpdateLangInfo())
                    UpateLanguageInfo();
            }

            //TODO: wtf!??

            if (IsRuLang())
            {
                AsyncOperationHandle handle = _serverData.LoadAssetAsync<GameObject>();
                handle.Completed += Handle_Completed;

                _loadingScreen.SetActive(true);
            }
        }

        public static bool IsRuLang()
        {
            // TODO: remove this test thing
            return false;

            return PlayerPrefs.GetInt(IsRUKey) == 1;
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
                App.ServerData = data;
            }
            else
            {
                Debug.LogError($"AssetReference {_serverData.RuntimeKey} failed to load.");
            }
        }

        private void UpateLanguageInfo() //TODO
        {
            if (Application.systemLanguage != SystemLanguage.Russian)
            {
                PlayerPrefs.SetInt(IsRUKey, 0);
                // TODO: need return?
            }

            if (CultureInfo.CurrentCulture.ToString() != "ru-RU")
            {
                PlayerPrefs.SetInt(IsRUKey, 0);
                // TODO: need return?
            }

            PlayerPrefs.SetInt(IsRUKey, 1);
        }
    }
}
