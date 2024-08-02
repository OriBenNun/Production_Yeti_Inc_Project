using System;
using UnityEngine;

namespace Game.Water_Area
{
    public class WaterPlayer : MonoBehaviour
    {
        [SerializeField] private int startingLives = 5;
        
        public static event Action<int> OnPlayerGotHit;
        public static event Action OnPlayerDied;
        
        private int _currentLives;

        private void Awake()
        {
            _currentLives = startingLives;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out Obstacle obstacle)) return;
            
            obstacle.Disable();
            _currentLives--;
            OnPlayerGotHit?.Invoke(_currentLives);

            if (_currentLives != 0) return;
            
            gameObject.SetActive(false);
            OnPlayerDied?.Invoke();
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
