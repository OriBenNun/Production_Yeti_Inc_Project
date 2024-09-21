using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Water_Area
{
    public class BoughtItemsUIManager : MonoBehaviour
    {
        [SerializeField] private Image netImage;
        [SerializeField] private Image ramImage;
        [SerializeField] private Image helmetImage;

        private void Awake()
        {
            CurrentRunDataHandler.OnBoughtItemsChanged += UpdateVisuals;
        }

        private void OnDestroy()
        {
            CurrentRunDataHandler.OnBoughtItemsChanged -= UpdateVisuals;
        }

        private void OnEnable()
        {
            UpdateVisuals();
        }

        private void UpdateVisuals()
        {
            netImage.gameObject.SetActive(CurrentRunDataHandler.HasNet);
            ramImage.gameObject.SetActive(CurrentRunDataHandler.HasRam);
            helmetImage.gameObject.SetActive(CurrentRunDataHandler.HasHelmet);
        }
    }
}
