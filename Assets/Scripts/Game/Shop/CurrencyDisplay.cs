using TMPro;
using UnityEngine;

namespace Game.Shop
{
    public class CurrencyDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currencyText;

        public void Awake()
        {
            CurrentRunDataHandler.OnCurrentCurrencyChanged += UpdateCurrencyText;

            UpdateCurrencyText(CurrentRunDataHandler.CurrentCurrency);
        }

        public void OnDestroy()
        {
            CurrentRunDataHandler.OnCurrentCurrencyChanged -= UpdateCurrencyText;
        }

        public void UpdateCurrencyText(int currency)
        {
            _currencyText.text = currency.ToString();
        }
    }
}
