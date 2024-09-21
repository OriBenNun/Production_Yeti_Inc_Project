using System;
using System.Collections.Generic;
using UnityEngine;

namespace Meta
{
    public class LeaderboardManager : MonoBehaviour
    {
        [SerializeField] private List<LeaderboardItem> leaderboardItems;

        private void Awake()
        {
            if (!PlayerPrefs.HasKey($"FirstDate")) return;
            
            var firstItem = leaderboardItems[0];
            firstItem.gameObject.SetActive(true);
            var firstDate = PlayerPrefs.GetString("FirstDate");
            var firstCubes = PlayerPrefs.GetInt("FirstCubes");
            var firstDistance = PlayerPrefs.GetInt("FirstDistance");
            firstItem.Init(firstDate, firstCubes, firstDistance);
            
            if (!PlayerPrefs.HasKey($"SecondDate")) return;
            
            var secondItem = leaderboardItems[1];
            secondItem.gameObject.SetActive(true);
            var secondDate = PlayerPrefs.GetString("SecondDate");
            var secondCubes = PlayerPrefs.GetInt("SecondCubes");
            var secondDistance = PlayerPrefs.GetInt("SecondDistance");
            secondItem.Init(secondDate, secondCubes, secondDistance);
            
            if (!PlayerPrefs.HasKey($"ThirdDate")) return;
            
            var thirdItem = leaderboardItems[2];
            thirdItem.gameObject.SetActive(true);
            var thirdDate = PlayerPrefs.GetString("ThirdDate");
            var thirdCubes = PlayerPrefs.GetInt("ThirdCubes");
            var thirdDistance = PlayerPrefs.GetInt("ThirdDistance");
            thirdItem.Init(thirdDate, thirdCubes, thirdDistance);
        }
    }
}
