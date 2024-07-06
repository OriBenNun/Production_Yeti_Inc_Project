using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Water_Area
{
    public class LaneManager : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private RectTransform playerPositionTransform;
        
        public static event Action<LaneManager> OnLaneClicked;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            OnLaneClicked?.Invoke(this);
        }

        public Vector3 GetPosition()
        {
            return new Vector3(playerPositionTransform.position.x, playerPositionTransform.position.y, 0);
        }
    }
}
