using System;
using UnityEngine;

namespace Game.Water_Area
{
    public class WaterPlayer : MonoBehaviour
    {
        public static event Action OnPlayerGotHit;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Obstacle"))
            {
                var obstacle = other.GetComponent<Obstacle>();
                OnPlayerGotHit?.Invoke();
                Debug.Log($"Got hit a obstacle: {obstacle.name}");
            }
        }

        public void MoveToLane(LaneManager currentLane)
        {
            var pos = currentLane.GetPlayerSpawnPosition();
            transform.position = new Vector3(pos.x, pos.y, 0);
        }

        public void MoveToPosition(Vector2 position)
        {
            transform.position = position;
        }
    }
}
