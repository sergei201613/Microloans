using UnityEngine;
using UnityEngine.Serialization;

namespace Sgorey.Microloans.Common
{
    public class ServerData : MonoBehaviour
    {
        public string[] Urls => _urls;

        [SerializeField] private string[] _urls;
    }
}
