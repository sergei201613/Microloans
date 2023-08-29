using Sgorey.Microloans.Common;
using UnityEngine;

namespace Sgorey.Microloans.Infrastructure
{
    public class App
    {
        public static ServerData ServerData;
        public static FakeWebviewsCanvas FakeWebview;
        public static bool IsFirstLaunch;

        private const string IsFirstLaunchKey = "IsFirstLaunch";

        public App()
        {
            Application.targetFrameRate = 60;

            IsFirstLaunch = !PlayerPrefs.HasKey(IsFirstLaunchKey);
            PlayerPrefs.SetInt(IsFirstLaunchKey, 1);
        }
    }
}
