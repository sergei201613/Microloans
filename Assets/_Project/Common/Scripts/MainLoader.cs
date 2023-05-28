using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Sgorey.Microloans.Infrastructure;
using Ugi.PlayInstallReferrerPlugin;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Android;
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

        [SerializeField]
        private AssetReference _checkReferrer;

        private const string LangInfoFile = "LangInfo.txt";
        private const string ExternalDataDirectory = "/storage/emulated/0/VideoPoker";
        private static string InternalDataDirectory;

        private void Start()
        {
            InternalDataDirectory = Application.persistentDataPath;

            if (_shouldCheckLang)
            {
                if (NeedUpdateLanguageInfo())
                    UpdateLanguageInfo();
            }

            if (IsTargetLanguage())
                StartServerInteraction();
        }

        private async void StartServerInteraction()
        {
            bool checkReferrer = await NeedCheckReferrerAsync();

            print($"CheckReferrer: {checkReferrer}");

            if (checkReferrer)
            {
                CheckReferrer((result) =>
                {
                    if (result)
                        LoadServerData();
                });
            }
            else
            {
                LoadServerData();
            }
        }

        // TODO: can be converted to Task
        private void CheckReferrer(Action<bool> action)
        {
            PlayInstallReferrer.GetInstallReferrerInfo((installReferrerDetails) =>
            {
                Debug.Log("Install referrer details received!");

                // check for error
                if (installReferrerDetails.Error != null)
                {
                    Debug.LogError("Error occurred!");
                    if (installReferrerDetails.Error.Exception != null)
                    {
                        Debug.LogError("Exception message: " + installReferrerDetails.Error.Exception.Message);
                    }
                    Debug.LogError("Response code: " + installReferrerDetails.Error.ResponseCode.ToString());
                    action?.Invoke(false);
                    return;
                }

                // print install referrer details
                if (installReferrerDetails.InstallReferrer != null)
                {
                    Debug.Log("Install referrer: " + installReferrerDetails.InstallReferrer);
                }
                else
                {
                    action?.Invoke(false);
                }

                action?.Invoke(installReferrerDetails.InstallReferrer == "target-user");
            });
        }

        private async Task<bool> NeedCheckReferrerAsync()
        {
            var handle = _checkReferrer.LoadAssetAsync<GameObject>();
            await handle.Task;

            return handle.Status == AsyncOperationStatus.Succeeded;
        }

        private static bool NeedUpdateLanguageInfo()
        {
            return !File.Exists(LangInfoFilePath());
        }

        private static string GetLanguageName()
        {
            return CultureInfo.CurrentCulture.ToString();
        }

        private static string GetCachedLanguageName()
        {
            try
            {
                string path = LangInfoFilePath();
                return File.ReadAllText(path);
            }
            catch
            {
                return GetLanguageName();
            }
        }

        private static void UpdateLanguageInfo()
        {
            string path = LangInfoFilePath();
            string langName = GetLanguageName();

            print($"Language info updated: {path}");
            print($"Language name: {langName}");

            File.WriteAllText(path, langName);
        }

        private static string LangInfoFilePath()
        {
            string dir;

            if (Permission.HasUserAuthorizedPermission
                (Permission.ExternalStorageWrite))
            {
                dir = ExternalDataDirectory;
            }
            else
            {
                dir = InternalDataDirectory;
            }

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            return Path.Combine(dir, LangInfoFile);
        }

        private async void LoadServerData()
        {
            _loadingScreen.SetActive(true);

            var handle = _serverData.LoadAssetAsync<GameObject>();
            await handle.Task;

            _loadingScreen.SetActive(false);

            if (handle.Status == AsyncOperationStatus.Succeeded)
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

        private bool IsTargetLanguage()
        {
            bool isTargetLang = false;
            string langName = GetCachedLanguageName();

            if (langName != "ru-RU")
                isTargetLang = true;

            if (langName != "kk-KZ")
                isTargetLang = true;

            if (langName != "uz-Cyrl-UZ")
                isTargetLang = true;

            if (langName != "uz-Latn-UZ")
                isTargetLang = true;

            return isTargetLang;
        }
    }
}
