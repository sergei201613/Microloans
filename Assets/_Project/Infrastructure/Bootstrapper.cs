using UnityEngine;

namespace Sgorey.Microloans.Infrastructure
{
    [DefaultExecutionOrder(-100)]
    public class Bootstrapper : MonoBehaviour
    {
        private App _app;
        
        private void Awake()
        {
            _app = new App();
        }
    }
}
