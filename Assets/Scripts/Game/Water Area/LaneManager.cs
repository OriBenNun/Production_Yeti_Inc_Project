using UnityEngine;

namespace Game.Water_Area
{
    public class LaneManager : MonoBehaviour
    {
        [SerializeField] private RectTransform playerPositionTransform;
        [SerializeField] private RectTransform obstacleSpawnPositionTransform;
        [SerializeField] private bool isEdgeLane;
        
        public bool IsEdgeLane() => isEdgeLane;
        
        public Vector2 GetPlayerSpawnPosition()
        {
            return playerPositionTransform.position;
        }

        public Vector2 GetObstacleSpawnPosition()
        {
            return obstacleSpawnPositionTransform.position;
        }
    }
}
