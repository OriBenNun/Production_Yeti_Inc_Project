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


        throw new System.NotImplementedException();
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
        SetAvailable(true);

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
        SetAvailable(true);

        if (TempDataHolder.CurrentCurrency >= Data.Price &&
            TempDataHolder.MaxLife - TempDataHolder.CurrentLife > 1)
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
        SetAvailable(true);

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
        SetAvailable(true);

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
        SetAvailable(true);

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
}
