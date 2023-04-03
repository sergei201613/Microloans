using UnityEngine;
using UnityEngine.Android;
using static UnityEngine.Android.Permission;

namespace Sgorey.Microloans.Infrastructure
{
    public class PermissionRequester : MonoBehaviour
    {
        [SerializeField] private GameObject _deniedPanel;
        [SerializeField] private GameObject _deniedAndDontAskPanel;

        private void Awake()
        {
            Request();
        }

        public void Request()
        {
            if (HasRequiredPermissions())
                return;

            var callbacks = new PermissionCallbacks();

            callbacks.PermissionDenied += PermissionDenied;
            callbacks.PermissionGranted += PermissionGranted;
            callbacks.PermissionDeniedAndDontAskAgain += PermissionDeniedAndDontAskAgain;

            string[] permissions = new string[]
            {
                ExternalStorageRead,
                ExternalStorageWrite,
                Permission.Camera
            };
            RequestUserPermissions(permissions, callbacks);
        }

        public void OpenAppSettings()
        {
            try
            {
#if UNITY_ANDROID
                using (var unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                using (AndroidJavaObject currentActivityObject = unityClass.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    string packageName = currentActivityObject.Call<string>("getPackageName");

                    using (var uriClass = new AndroidJavaClass("android.net.Uri"))
                    using (AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("fromParts", "package", packageName, null))
                    using (var intentObject = new AndroidJavaObject("android.content.Intent", "android.settings.APPLICATION_DETAILS_SETTINGS", uriObject))
                    {
                        intentObject.Call<AndroidJavaObject>("addCategory", "android.intent.category.DEFAULT");
                        intentObject.Call<AndroidJavaObject>("setFlags", 0x10000000);
                        currentActivityObject.Call("startActivity", intentObject);
                    }
                }
#endif
            }
            catch (System.Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        private static bool HasRequiredPermissions()
        {
            return HasUserAuthorizedPermission(ExternalStorageRead)
                && HasUserAuthorizedPermission(ExternalStorageWrite)
                && HasUserAuthorizedPermission(Permission.Camera);
        }

        private void PermissionGranted(string permissionName)
        {
            Debug.Log($"{permissionName} PermissionCallbacks_PermissionGranted");

            if (HasRequiredPermissions() && HasCamera())
            {
                _deniedPanel.SetActive(false);
                _deniedAndDontAskPanel.SetActive(false);
            }
        }

        private bool HasCamera()
        {
            return WebCamTexture.devices.Length > 0;
        }

        private void PermissionDenied(string permissionName)
        {
            //_deniedPanel.SetActive(true);
            Debug.Log($"{permissionName} PermissionCallbacks_PermissionDenied");
        }

        private void PermissionDeniedAndDontAskAgain(string permissionName)
        {
            _deniedAndDontAskPanel.SetActive(true);
            Debug.Log($"{permissionName} PermissionDeniedAndDontAskAgain");
        }
    }
}
