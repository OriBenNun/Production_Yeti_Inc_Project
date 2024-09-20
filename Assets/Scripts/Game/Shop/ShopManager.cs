using System.Collections.Generic;
using UnityEngine;

namespace Game.Shop
{
    public class ShopManager : MonoBehaviour
    {
        [SerializeField] private CheckoutWindow _checkoutWindow;
        [SerializeField] private CurrencyDisplay _currencyDisplay;
        [SerializeField] private List<ShopItem> _items;

        // Replace this with the way to get and set the currency in the game.
        public int CurrentCurrency { get { return CurrentRunDataHandler.CurrentCurrency; } }

        private void Awake()
        {
            

            _checkoutWindow.Init(this);
            foreach (ShopItem item in _items)
            {
                item.Init(this);
                item.SetAvailable(true);
            }

            _currencyDisplay.UpdateCurrencyText(CurrentCurrency);
        }


        public void OpenCheckout(ShopItem item)
        {
            _checkoutWindow.StartCheckout(item);
        }

        internal void OnPurchase()
        {
            _currencyDisplay.UpdateCurrencyText(CurrentCurrency);
            foreach(ShopItem item in _items)
            {
                item.UpdateState();
            }
        }
    }
}
