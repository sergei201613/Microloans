using UnityEngine;
using UnityEngine.UI;

namespace Sgorey.Microloans.Infrastructure
{
    [RequireComponent(typeof(Button))]
    public class PermissionRequesterButton : MonoBehaviour
    {
        private Button _button;
        private PermissionRequester _requester;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _requester = FindObjectOfType<PermissionRequester>();

            if (_requester == null)
                Destroy(this);
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(Request);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Request);
        }

        private void Request()
        {
            _requester.Request();
        }
    }
}
