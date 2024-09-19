using TMPro;
using UnityEngine;

namespace Game.Shop
{
    public class CurrencyDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currencyText;


        public void UpdateCurrencyText(int currency)
        {
            _currencyText.text = currency.ToString();
        }
    }
}
