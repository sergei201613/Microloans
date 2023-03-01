using System.Globalization;
using Sgorey.Microloans.Infrastructure;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Sgorey.Microloans.Common
{
    public class MainLoader : MonoBehaviour
    {
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
                if (Application.systemLanguage != SystemLanguage.Russian)
                    return;

                if (CultureInfo.CurrentCulture.ToString() != "ru-RU")
                    return;
            }

            AsyncOperationHandle handle = _serverData.LoadAssetAsync<GameObject>();
            handle.Completed += Handle_Completed;
            
            _loadingScreen.SetActive(true);
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
    }
}
