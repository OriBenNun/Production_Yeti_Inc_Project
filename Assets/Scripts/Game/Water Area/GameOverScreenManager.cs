using System;
using TMPro;
using UnityEngine;

namespace Game.Water_Area
{
    public class GameOverScreenManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text distanceText;
        [SerializeField] private TMP_Text timeText;
        [SerializeField] private TMP_Text iceCubesText;
        
        private void OnEnable()
        {
            distanceText.text = $"Distance: {CurrentRunDataHandler.DistanceTraveled}m";
            
            var timeInMinutes = CurrentRunDataHandler.CurrentRunTime / 60;
            timeText.text = $"Time Played: {timeInMinutes:F1} min(s)";
            
            iceCubesText.text = $"Ice Cubes Collected: {CurrentRunDataHandler.CurrentCurrency}";
        }
    }
}
