using UnityEngine;

namespace Game.Water_Area
{
    public class LaneManager : MonoBehaviour
    {
        [SerializeField] private RectTransform playerPositionTransform;
        
        public Vector3 GetPosition()
        {
            return playerPositionTransform.position;
        }
    }
}
