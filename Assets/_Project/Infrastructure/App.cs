using Sgorey.Microloans.Common;
using UnityEngine;

namespace Sgorey.Microloans.Infrastructure
{
    public class App
    {
        public static ServerData ServerData;
        public static FakeWebviewsCanvas FakeWebview;

        public App()
        {
            Application.targetFrameRate = 60;
        }
    }
}
