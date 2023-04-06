using UnityEngine;

namespace Sgorey.Microloans
{
    public class PersistentPanels : MonoBehaviour
    {
        public static PersistentPanels Instance { get; private set; }

        [field: SerializeField]
        public MicroloanDescriptionPanel MicroloanDescription { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}
