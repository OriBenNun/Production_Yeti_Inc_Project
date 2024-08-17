using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

        if (TempDataHolder.CurrentCurrency >= Data.Price && 
            TempDataHolder.MaxLife - TempDataHolder.CurrentLife > 0)
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

        if (TempDataHolder.CurrentCurrency >= Data.Price &&
            TempDataHolder.MaxLife - TempDataHolder.CurrentLife > 0)
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

        if (TempDataHolder.CurrentCurrency >= Data.Price &&
            !TempDataHolder.HasRam)
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

        if (TempDataHolder.CurrentCurrency >= Data.Price &&
            !TempDataHolder.HasHelmet)
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

        if (TempDataHolder.CurrentCurrency >= Data.Price &&
             !TempDataHolder.HasNet)
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
        TempDataHolder.CurrentCurrency -= Data.Price;

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
        TempDataHolder.CurrentLife += 1;
    }
    private void OnPurchaseWoodBundle()
    {
        TempDataHolder.CurrentLife += 2;
    }
    private void OnPurchaseNet()
    {
        TempDataHolder.HasNet = true;
    }
    private void OnPurchaseMetalRam()
    {
        TempDataHolder.HasRam = true;
    }
    private void OnPurchaseHelmet()
    {
        TempDataHolder.HasHelmet = true;
    }

}
