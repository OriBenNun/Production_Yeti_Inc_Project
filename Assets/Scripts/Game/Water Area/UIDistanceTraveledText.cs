using TMPro;
using UnityEngine;

namespace Game.Water_Area
{
    public class UIDistanceTraveledText : MonoBehaviour
    {
        [SerializeField] private TMP_Text distanceTraveledText;

        private void LateUpdate()
        {
            distanceTraveledText.text = GameLoopManager.DistanceTraveled.ToString("N0");
        }
    }
}
