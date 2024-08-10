using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Game.Water_Area
{
    public class WaterAreaCharacterController : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        [FormerlySerializedAs("player")] [SerializeField] private WaterPlayer waterPlayer;
        [SerializeField] private List<LaneManager> lanes;
        [SerializeField] private Transform startingPosition;
        [SerializeField] private float swipeDetectionSensitivity = 1f;
        
        private LaneManager _currentLane;
        private float _touchStartPosition;
        private bool _isDragging;
        
        private bool _isDisabled;

        private void Start()
        {
            _currentLane = lanes[1];
            MovePlayerToStartingPosition();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_isDisabled) return;
            
            _touchStartPosition = eventData.position.x;
            _isDragging = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_isDisabled) return;
            
            if (!_isDragging) { return; }
            
            var touchPosition = eventData.position.x;
            var swipeDistance = Mathf.Abs(_touchStartPosition -touchPosition);

            if (swipeDistance <= swipeDetectionSensitivity)
            {
                return;
            }
            
            var swipeDirection = _touchStartPosition - touchPosition < 0 ? SwipeDirection.Right : SwipeDirection.Left;
            var lane = GetLaneBySwipeDirection(swipeDirection);
            UpdateCurrentLaneAndMovePlayer(lane);
            _isDragging = false;
        }
        
        public void DisableControls()
        {
            _isDisabled = true;
        }
        
        public List<LaneManager> GetLanes() => lanes;
        
        public LaneManager GetRandomLane()
        {
            var randomIndex = Random.Range(0, lanes.Count);
            return lanes[randomIndex];
        }
        
        public LaneManager GetRandomEdgeLane()
        {
            var edgeLanes = new List<LaneManager>();
            foreach (var lane in lanes)
            {
                switch (lane.GetLanePositionType())
                {
                    case LanePosition.Center:
                        break;
                    case LanePosition.Left:
                    case LanePosition.Right:
                        edgeLanes.Add(lane);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            var randomIndex = Random.Range(0, edgeLanes.Count);
            return edgeLanes[randomIndex];
        }
        
        private void MovePlayerToStartingPosition()
        {
            waterPlayer.StartMoveToPosition(startingPosition.position);
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
