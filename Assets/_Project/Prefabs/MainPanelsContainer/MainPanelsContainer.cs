using UnityEngine;

namespace Sgorey.Microloans
{
    public class MainPanelsContainer : MonoBehaviour
    {
        [SerializeField] private BankInfoPanelContainer[] _infoContainers;

        public void Refresh()
        {
            foreach (var container in _infoContainers)
            {
                container.InvokeUpdated();
            }
        }
    }
}
