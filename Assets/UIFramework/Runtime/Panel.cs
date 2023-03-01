using UnityEngine;

namespace Sgorey.UIFramework.Runtime
{
    public class Panel : MonoBehaviour
    {
        private PanelManager _panelManager;

        public virtual void Init(PanelManager panelManager)
        {
            _panelManager = panelManager;
        }

        public void Back()
        {
            _panelManager.Back(out var _);
        }
    }
}
