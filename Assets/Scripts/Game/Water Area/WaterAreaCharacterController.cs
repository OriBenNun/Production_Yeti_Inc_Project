using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Game.Water_Area
{
    public class WaterAreaCharacterController : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        [FormerlySerializedAs("player")] [SerializeField] private WaterPlayer waterPlayer;
        [SerializeField] private List<LaneManager> lanes;
        [SerializeField] private Transform startingPosition;
        [SerializeField] private float swipeDetectionSensitivity = 1f;
        
        private LaneManager _currentLane;
        private Vector2 _touchStartPosition;
        private bool _isDragging;

        private void Start()
        {
            _currentLane = lanes[1];
            MovePlayerToStartingPosition();
        }

        private void MovePlayerToStartingPosition()
        {
            waterPlayer.MoveToPosition(startingPosition.position);
        }

        private void UpdateCurrentLaneAndMovePlayer(LaneManager lane)
        {
            _currentLane = lane;
            MovePlayerToCurrentLane();
        }

        private void MovePlayerToCurrentLane()
        {
            waterPlayer.MoveToLane(_currentLane);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _touchStartPosition = eventData.position;
            _isDragging = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_isDragging)
            {
                _touchStartPosition = eventData.position;
                _isDragging = true;
            }
            
            var touchPosition = eventData.position;
            var swipeDistance = touchPosition - _touchStartPosition;

            if (swipeDistance.magnitude > swipeDetectionSensitivity)
            {
                var swipeDirection = swipeDistance.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
                var lane = GetLaneBySwipeDirection(swipeDirection);
                UpdateCurrentLaneAndMovePlayer(lane);
                _isDragging = false;
            }
        }

        private LaneManager GetLaneBySwipeDirection(SwipeDirection swipeDirection)
        {
            var currentLaneIndex = lanes.IndexOf(_currentLane);
            var nextLaneIndex = swipeDirection == SwipeDirection.Left ? currentLaneIndex - 1 : currentLaneIndex + 1;

            if (nextLaneIndex < 0 || nextLaneIndex >= lanes.Count)
            {
                return _currentLane;
            }
            
            return lanes[nextLaneIndex];
        }
    }
    
    public enum SwipeDirection
    {
        Left,
        Right
    }
}
