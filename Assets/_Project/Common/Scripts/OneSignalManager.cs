using UnityEngine;
using OneSignalSDK;

namespace Sgorey.Microloans
{
    public class OneSignalManager : MonoBehaviour
    {
        void Start()
        {
            // Replace 'YOUR_ONESIGNAL_APP_ID' with your OneSignal App ID from app.onesignal.com
            OneSignal.Default.Initialize("YOUR_ONESIGNAL_APP_ID");
        }
    }
}
