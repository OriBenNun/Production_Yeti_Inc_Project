using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class WaterAreaCharacterController : MonoBehaviour
    {
        [SerializeField] private List<LaneManager> lanes;
        // [SerializeField] private 
        
        private void Awake()
        {
            foreach (var lane in lanes)
            {
                lane.OnLaneClicked += HandleOnLaneClicked;
            }
        }

        private void HandleOnLaneClicked()
        {
            
        }
    }
}
