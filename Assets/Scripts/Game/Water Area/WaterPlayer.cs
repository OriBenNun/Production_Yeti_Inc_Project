using UnityEngine;

namespace Game.Water_Area
{
    public class WaterPlayer : MonoBehaviour
    {
        public void MoveToLane(LaneManager currentLane)
        {
            transform.position = currentLane.GetPosition();
        }
    }
}
