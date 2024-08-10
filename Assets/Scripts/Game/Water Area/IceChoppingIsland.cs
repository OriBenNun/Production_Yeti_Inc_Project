using UnityEngine;

namespace Game.Water_Area
{
    public class IceChoppingIsland : WaterAreaStopIsland
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float spriteShift = 0.4f;
        
        public void Init(float islandSpeed, float yPositionToStop, LaneManager laneToSpawn)
        {
            InitVisuals(laneToSpawn);
            base.Init(islandSpeed, yPositionToStop);
        }

        private void InitVisuals(LaneManager laneToSpawn)
        {
            var localPos = spriteRenderer.transform.localPosition;
            localPos.x = spriteShift * (laneToSpawn.GetLanePositionType() == LanePosition.Left ? -1 : 1);
            spriteRenderer.transform.localPosition = localPos;
            spriteRenderer.flipX = laneToSpawn.GetLanePositionType() == LanePosition.Left;
        }
    }
}
