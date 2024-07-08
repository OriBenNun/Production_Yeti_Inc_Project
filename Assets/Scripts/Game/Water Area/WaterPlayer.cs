using UnityEngine;

namespace Game.Water_Area
{
    public class WaterPlayer : MonoBehaviour
    {
        public void MoveToLane(LaneManager currentLane)
        {
            var pos = currentLane.GetPosition();
            transform.position = new Vector3(pos.x, pos.y, 0);
        }

        public void MoveToPosition(Vector2 position)
        {
            transform.position = position;
        }
    }
}
