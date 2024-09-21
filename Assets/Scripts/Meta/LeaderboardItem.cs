using System;
using TMPro;
using UnityEngine;

namespace Meta
{
    [Serializable]
    public class LeaderboardItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text dateText;
        [SerializeField] private TMP_Text cubesText;
        [SerializeField] private TMP_Text distanceText;

        public void Init(string date, int cubes, int distance)
        {
            dateText.text = date;
            cubesText.text = cubes.ToString("N0");
            distanceText.text = $"{distance:N0}m";
        }
    }
}
