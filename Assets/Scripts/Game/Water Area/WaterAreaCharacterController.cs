using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Water_Area
{
    public class WaterAreaCharacterController : MonoBehaviour
    {
        [FormerlySerializedAs("player")] [SerializeField] private WaterPlayer waterPlayer;
        [SerializeField] private LaneManager startLane;
        
        private LaneManager _currentLane;
        
        private void Awake()
        {
            LaneManager.OnLaneClicked += HandleOnLaneClicked;
            _currentLane = startLane;
        }

        private void Start()
        {
            MovePlayerToCurrentLane();
        }

        private void HandleOnLaneClicked(LaneManager lane)
        {
            Debug.Log($"Clicked on lane: {lane.name}");
            _currentLane = lane;
            
            MovePlayerToCurrentLane();
        }

        private void MovePlayerToCurrentLane()
        {
            waterPlayer.MoveToLane(_currentLane);
        }
    }
}
