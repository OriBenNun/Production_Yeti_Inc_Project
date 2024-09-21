using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Shop
{
    public class CheckoutWindow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _descText;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Button _buyButton;
        [SerializeField] private TMP_Text _confirmText;

        private ShopItem _shopItem;
        private ShopManager _shopManager;

        public void Init(ShopManager shopManager)
        {
            _shopManager = shopManager;
            gameObject.SetActive(false);
        }

        public void StartCheckout(ShopItem shopItem)
        {
            _shopItem = shopItem;

            _nameText.text = _shopItem.Data.Name;
            _descText.text = _shopItem.Data.Description;
            _priceText.text = _shopItem.Data.Price.ToString();

            if(CurrentRunDataHandler.CurrentCurrency >= _shopItem.Data.Price )
            {
                _buyButton.interactable = true;
                _confirmText.color = Color.white;
            }
            else
            {
                _buyButton.interactable = false;
                _confirmText.color = Color.gray;
            }
            gameObject.SetActive(true);
        }

        public void OnConfirm()
        {
            _shopItem.OnPurchase();
            _shopManager.OnPurchase();
            gameObject.SetActive(false);
        }

        public void OnCancel()
        {
            gameObject.SetActive(false);
        }




    }
}
