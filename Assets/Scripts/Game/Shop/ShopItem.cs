using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private ShopItemData _itemData;
    public ShopItemData Data { get { return _itemData; } }

    private bool _isPurchasable = true;
    private bool _isAvailable = true;

    
    
    public void SetPurchasable(bool isPurchasable)
    {
        _isPurchasable = isPurchasable;
    }
    public void SetAvailable(bool isAvailable)
    {
        _isAvailable = isAvailable;
        gameObject.SetActive(isAvailable);
    }

}
