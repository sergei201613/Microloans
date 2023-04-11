using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sgorey.Microloans
{
    public class BankInfoPanel : MonoBehaviour
    {
        public string Title => _titleText.text;

        public int ID => _id;

        public bool IsFavorite
        {
            get
            {
                return PlayerPrefs.HasKey(GetUniqueKey());
            }
            set
            {
                if (value)
                    PlayerPrefs.SetInt(GetUniqueKey(), 0);
                else
                    PlayerPrefs.DeleteKey(GetUniqueKey());

                _isFavorite = value;
                _container.InvokeUpdated();
            }
        }

        [SerializeField] private int _id;
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private Button _heartButton;
        [SerializeField] private Image _heartButtonImage;
        [SerializeField] private Sprite _heartOffSprite;
        [SerializeField] private Sprite _heartOnSprite;
        [SerializeField] private BankInfoPanelContainer _container;

        private bool _isFavorite;

        private void OnEnable()
        {
            _heartButton.onClick.AddListener(ToggleFavorite);
            _container.Updated += Refresh;
            UpdateHeartImage();
        }

        private void OnDisable()
        {
            _container.Updated -= Refresh;
            _heartButton.onClick.RemoveListener(ToggleFavorite);
        }

        private void ToggleFavorite()
        {
            IsFavorite = !IsFavorite;

            UpdateHeartImage();
        }

        private void UpdateHeartImage()
        {
            _heartButtonImage.sprite = IsFavorite ? _heartOnSprite : _heartOffSprite;
        }

        private string GetUniqueKey()
        {
            return $"FavoriteBankInfoPanel_{_id}";
        }

        private void Refresh()
        {
            UpdateHeartImage();
        }
    }
}
