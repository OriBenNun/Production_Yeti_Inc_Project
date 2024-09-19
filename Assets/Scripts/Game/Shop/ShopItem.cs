using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Shop
{
    public class ShopItem : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private ShopItemData _itemData;
        public ShopItemData Data { get { return _itemData; } }


        [SerializeField] private Color NormalColor;
        [SerializeField] private Color BlockedColor;


        private ShopManager _shopManager;
        private bool _isPurchasable = true;
        private bool _isAvailable = true;

    
        public void Init(ShopManager shopManager)
        {
            _shopManager = shopManager;
            UpdateState();
        }

        public void SetPurchasable(bool isPurchasable)
        {
            _isPurchasable = isPurchasable;

            if (_isPurchasable)
            {
                _itemImage.color = NormalColor;
            }
            else
            {
                _itemImage.color = BlockedColor;
            }
        }
        public void SetAvailable(bool isAvailable)
        {
            _isAvailable = isAvailable;
            gameObject.SetActive(isAvailable);

        
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_isPurchasable) return;
            _shopManager.OpenCheckout(this);
        }

        public void UpdateState()
        {
            switch (Data.Enum)
            {
                case ItemEnums.Wood:
                    UpdateWood();
                    break;
                case ItemEnums.WoodBundle:
                    UpdateWoodBundle();
                    break;
                case ItemEnums.Net:
                    UpdateNet();
                    break;
                case ItemEnums.Helmet:
                    UpdateHelmet();
                    break;
                case ItemEnums.MetalRam:
                    UpdateRam();
                    break;
            }
        }

        private void UpdateWood()
        {

            if (CurrentRunDataHandler.CurrentCurrency >= Data.Price && 
                CurrentRunDataHandler.MaxLife - CurrentRunDataHandler.CurrentLife > 0)
            {
                SetPurchasable(true);
            }
            else
            {
                SetPurchasable(false);
            }
        }

        private void UpdateWoodBundle()
        {

            if (CurrentRunDataHandler.CurrentCurrency >= Data.Price &&
                CurrentRunDataHandler.MaxLife - CurrentRunDataHandler.CurrentLife > 0)
            {
                SetPurchasable(true);
            }
            else
            {
                SetPurchasable(false);
            }
        }

        private void UpdateRam()
        {

            if (CurrentRunDataHandler.CurrentCurrency >= Data.Price &&
                !CurrentRunDataHandler.HasRam)
            {
                SetPurchasable(true);
            }
            else
            {
                SetPurchasable(false);
            }
        }

        private void UpdateHelmet()
        {

            if (CurrentRunDataHandler.CurrentCurrency >= Data.Price &&
                !CurrentRunDataHandler.HasHelmet)
            {
                SetPurchasable(true);
            }
            else
            {
                SetPurchasable(false);
            }
        }

        private void UpdateNet()
        {

            if (CurrentRunDataHandler.CurrentCurrency >= Data.Price &&
                !CurrentRunDataHandler.HasNet)
            {
                SetPurchasable(true);
            }
            else
            {
                SetPurchasable(false);
            }
        }


        public void OnPurchase()
        {
            CurrentRunDataHandler.CurrentCurrency -= Data.Price;

            switch (Data.Enum)
            {
                case ItemEnums.Wood:
                    OnPurchaseWood();
                    break;
                case ItemEnums.WoodBundle:
                    OnPurchaseWoodBundle();
                    break;
                case ItemEnums.Net:
                    OnPurchaseNet();
                    break;
                case ItemEnums.Helmet:
                    OnPurchaseHelmet();
                    break;
                case ItemEnums.MetalRam:
                    OnPurchaseMetalRam();
                    break;
            }

            SetAvailable(false);
        }
        private void OnPurchaseWood()
        {
            CurrentRunDataHandler.CurrentLife += 1;
        }
        private void OnPurchaseWoodBundle()
        {
            CurrentRunDataHandler.CurrentLife += 2;
        }
        private void OnPurchaseNet()
        {
            CurrentRunDataHandler.HasNet = true;
        }
        private void OnPurchaseMetalRam()
        {
            CurrentRunDataHandler.HasRam = true;
        }
        private void OnPurchaseHelmet()
        {
            CurrentRunDataHandler.HasHelmet = true;
        }

    }
}
