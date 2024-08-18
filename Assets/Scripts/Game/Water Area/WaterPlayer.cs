using System;
using System.Collections;
using Game.Water_Area.Obstacles;
using UnityEngine;

namespace Game.Water_Area
{
    public class WaterPlayer : MonoBehaviour
    {
        [SerializeField] private int startingLives = 5;
        [SerializeField] private float moveSpeed = 1.0f;
        [SerializeField] private float moveToIslandSpeed = 0.5f;
        [SerializeField] private float moveToIslandDestinationShift = 0.5f;
        
        public static event Action<int> OnPlayerGotHit;
        public static event Action OnPlayerDied;

        public static event Action<WaterAreaStopIsland> OnPlayerReachedIsland;
        
        public static int Lives { get; private set; }
        
        private Coroutine _moveCoroutine;
        
        private void Awake()
        {
            if (Lives == 0)
            { 
                Lives = startingLives;
            }

            GameLoopManager.OnGameQuited += HandleOnGameQuited;
        }

        private void OnDestroy()
        {
            GameLoopManager.OnGameQuited -= HandleOnGameQuited;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out Obstacle obstacle)) return;
            
            obstacle.Disable();
            Lives--;
            OnPlayerGotHit?.Invoke(Lives);

            if (Lives != 0) return;
            
            gameObject.SetActive(false);
            OnPlayerDied?.Invoke();
        }

        public void MoveToLane(LaneManager currentLane)
        {
            var pos = currentLane.GetPlayerSpawnPosition();
            StartMoveToPosition(pos);
        }
        
        public void StartMoveToPosition(Vector2 position)
        {
            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
            }
            _moveCoroutine = StartCoroutine(StartMoveTowardsPosition(position));
        }
        
        public void StartMoveToIsland(WaterAreaStopIsland island)
        {
            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
            }
            _moveCoroutine = StartCoroutine(StartMoveTowardsIsland(island));
        }

        private IEnumerator StartMoveTowardsPosition(Vector2 position)
        {
            while ((Vector2)transform.position != position)
            {
                transform.position = Vector3.MoveTowards(transform.position, position, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
        
        private IEnumerator StartMoveTowardsIsland(WaterAreaStopIsland island)
        {
            var position = island.GetPosition();
            var shiftEdge = moveToIslandDestinationShift * (position.x < 0 ? 1 : -1);
            position.x += shiftEdge;
            while ((Vector2)transform.position != position)
            {
                transform.position = Vector3.MoveTowards(transform.position, position, moveToIslandSpeed * Time.deltaTime);
                yield return null;
            }
            
            OnPlayerReachedIsland?.Invoke(island);
        }
        
        private void HandleOnGameQuited()
        {
            Lives = startingLives;
        }
    }
}
