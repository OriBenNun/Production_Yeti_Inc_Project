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
            netImage.enabled = CurrentRunDataHandler.HasNet;
            ramImage.enabled = CurrentRunDataHandler.HasRam;
            helmetImage.enabled = CurrentRunDataHandler.HasHelmet;
        }
    }
}
