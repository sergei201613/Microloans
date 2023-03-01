using Sgorey.Microloans.Common;
using Sgorey.UIFramework.Runtime;
using UnityEngine;

namespace Sgorey.Microloans.Infrastructure
{
    public class App
    {
        public static readonly PanelManager PanelManager;

        // TODO: shit code, because not readonly.
        public static ServerData ServerData;

        static App()
        {
            PanelManager = Object.FindObjectOfType<PanelManager>();
        }
    }
}
