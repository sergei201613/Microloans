using Sgorey.Microloans.Infrastructure;
using UnityEngine;

namespace Sgorey.Microloans.Common
{
    public class ServerData : MonoBehaviour, IService
    {
        public string[] Urls => _urls;

        [SerializeField] private string[] _urls;
    }
}
