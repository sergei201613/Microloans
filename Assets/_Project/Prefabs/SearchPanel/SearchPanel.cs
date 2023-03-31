using TMPro;
using UnityEngine;

namespace Sgorey.Microloans
{
    public class SearchPanel : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Transform _content;
        [SerializeField] private bool _onlyFavorites;
        [SerializeField] private GameObject _noFavoritesPanel;

        private BankInfoPanelContainer _container;

        private void Awake()
        {
            _container = _content.GetComponent<BankInfoPanelContainer>();
        }

        private void OnEnable()
        {
            Filter();
            _inputField.onValueChanged.AddListener(Filter);
            _container.Updated += Filter;
        }

        private void OnDisable()
        {
            _inputField.onValueChanged.RemoveListener(Filter);
            _container.Updated -= Filter;
        }

        private void Filter() => Filter(_inputField.text);

        private void Filter(string title)
        {
            title = title.Trim().ToLower();

            foreach (Transform t in _content)
                t.gameObject.SetActive(true);

            FilterByTitle(title);

            if (_onlyFavorites)
            {
                int favorites = FilterByFavorites();
                _noFavoritesPanel.SetActive(favorites == 0);
            }
        }

        private int FilterByFavorites()
        {
            int favorites = 0;
            foreach (Transform t in _content.transform)
            {
                if (t.TryGetComponent<BankInfoPanel>(out var bankInfoPanel))
                {
                    if (bankInfoPanel.IsFavorite)
                        favorites++;
                    else
                        t.gameObject.SetActive(false);
                }
            }
            return favorites;
        }

        private void FilterByTitle(string title)
        {
            foreach (Transform t in _content.transform)
            {
                if (t.TryGetComponent<BankInfoPanel>(out var bankInfoPanel))
                {
                    bool active = bankInfoPanel.Title.Trim().ToLower().Contains(title);

                    if (!active)
                        t.gameObject.SetActive(false);
                }
            }
        }
    }
}
