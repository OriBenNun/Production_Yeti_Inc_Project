using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class LaneManager : MonoBehaviour, IPointerDownHandler
    {
        public event Action OnLaneClicked;
        public void OnPointerDown(PointerEventData eventData)
        {
            OnLaneClicked?.Invoke();
        }
    }
}
