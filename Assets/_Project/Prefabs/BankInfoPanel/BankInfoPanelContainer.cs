using UnityEngine;

namespace Sgorey.Microloans
{
    public class BankInfoPanelContainer : MonoBehaviour
    {
        public event System.Action Updated;

        public void InvokeUpdated()
        {
            Updated?.Invoke();
        }
    }
}
