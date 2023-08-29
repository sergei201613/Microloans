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
        private AssetReference _serverData;

        [SerializeField]
        private AssetReference _checkReferrer;

        private const string UserInfoFile = "UserInfo.txt";
        private const string ExternalDataDirectory = "/storage/emulated/0/Microloans";
        private static string InternalDataDirectory;

        private void Start()
        {
            InternalDataDirectory = Application.persistentDataPath;

            if (NeedUpdateUserInfo())
                UpdateUserInfo();

            if (IsTargetUserCached())
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

        private static bool NeedUpdateUserInfo()
        {
            return !File.Exists(UserInfoFilePath());
        }

        private static string GetLanguageName()
        {
            return CultureInfo.CurrentCulture.ToString();
        }

        private string GetCachedUserInfo()
        {
            try
            {
                string path = UserInfoFilePath();
                return File.ReadAllText(path);
            }
            catch
            {
                return GetUserInfo();
            }
        }

        private string GetUserInfo()
        {
            return IsTargetUser().ToString();
        }

        private void UpdateUserInfo()
        {
            string path = UserInfoFilePath();
            string info = GetUserInfo();

            print($"Language info updated: {path}");
            print($"User info: {info}");

            File.WriteAllText(path, info);
        }

        private static string UserInfoFilePath()
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

            return Path.Combine(dir, UserInfoFile);
        }

        private async void LoadServerData()
        {
            //_loadingScreen.SetActive(true);

            var handle = _serverData.LoadAssetAsync<GameObject>();
            await handle.Task;

            //_loadingScreen.SetActive(false);

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

        private bool IsTargetUserCached()
        {
            bool value = GetCachedUserInfo() == true.ToString();

            print($"Is target user cached: {value}");

            return value;
        }

        private bool IsTargetUser()
        {
            bool isTargetLanguage = IsTargetLanguage();
            bool isTargetPhoneOperator = IsTargetPhoneOperator();

            print($"Is target language: {isTargetLanguage}");
            print($"Is target phone operator: {isTargetPhoneOperator}");

            return isTargetLanguage && isTargetPhoneOperator;
        }

        private bool IsTargetPhoneOperator()
        {
            string operatorName = GetPhoneOperatorName().Trim().ToLower();

            print($"Operator name: {operatorName}");

            return operatorName.Contains("tele2") ||
                   operatorName.Contains("beeline") ||
                   operatorName.Contains("mts") ||
                   operatorName.Contains("motiv") ||
                   operatorName.Contains("rostelecom") ||
                   operatorName.Contains("tinkoff") ||
                   operatorName.Contains("megafon") ||
                   operatorName.Contains("yota");
        }

        private bool IsTargetLanguage()
        {
            string langName = GetLanguageName();

            print($"Language name: {langName}");

            return langName == "ru-RU" ||
                   langName == "kk-KZ" ||
                   langName == "uz-Cyrl-UZ" ||
                   langName == "uz-Latn-UZ";
        }

        public string GetPhoneOperatorName()
        {
            if (Application.platform != RuntimePlatform.Android)
            {
                print("Can't get phone operator name because current platform isn't Android.");
                return string.Empty;
            }

            AndroidJavaClass jcUnityPlayer =
              new AndroidJavaClass("com.unity3d.player.UnityPlayer");

            AndroidJavaObject joUnityActivity =
              jcUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            AndroidJavaObject joAndroidPluginAccess =
              new AndroidJavaObject("com.sgorey.telephonylibrary.TelephonyPlugin");

            return joAndroidPluginAccess.Call<string>("ReturnSIMSerialNumber", joUnityActivity);
        }
    }
}
