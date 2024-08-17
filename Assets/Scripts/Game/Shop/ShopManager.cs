using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{

    [SerializeField] private CurrencyDisplay _currencyDisplay;
    [SerializeField] private List<ShopItem> _items;

    private void Awake()
    {
        TempDataHolder.Init();

        foreach (ShopItem item in _items)
        {
            item.Init(this);
        }

        _currencyDisplay.UpdateCurrencyText(CurrentCurrency);
    }

    // Replace this with the way to get and set the currency in the game.
    public int CurrentCurrency { get { return TempDataHolder.CurrentCurrency; } }

}
