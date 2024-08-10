using System.Collections;
using System.Collections.Generic;
using Pool_System;
using UnityEngine;

namespace Game.Water_Area.Obstacles
{
    public class ObstaclesPoolSpawner : ObjectPool<Obstacle>
    {
        [SerializeField] private Obstacle obstaclePrefab;
        [SerializeField] private int initialPoolSize = 50;
        [SerializeField] private WaterAreaCharacterController waterAreaCharacterController;

        private List<Sprite> _obstacleSprites;

        private Coroutine _spawnRandomObstaclesWithCooldown;

        private void Awake()
        {
            InitPool(initialPoolSize, obstaclePrefab, transform);
        }

        public void StartSpawningRandomObstacles(float spawnCooldown, List<Sprite> obstaclesSprites, float speed)
        {
            _obstacleSprites = obstaclesSprites;
            _spawnRandomObstaclesWithCooldown = StartCoroutine(SpawnRandomObstaclesWithCooldown(spawnCooldown, speed));
        }

        public void StopSpawning()
        {
            if (_spawnRandomObstaclesWithCooldown != null)
            {
                StopCoroutine(_spawnRandomObstaclesWithCooldown);
            }
        }

        private Obstacle SpawnNewObstacle(LaneManager lane, float speed)
        {
            var obstacle = GetReadyObject();
            obstacle.SetPosition(lane.GetObstacleSpawnPosition());
            obstacle.gameObject.SetActive(true);
            var randomObstacleSprite = _obstacleSprites[Random.Range(0, _obstacleSprites.Count)];
            obstacle.Init(10, randomObstacleSprite, speed);
            return obstacle;
        }

        private IEnumerator SpawnRandomObstaclesWithCooldown(float cooldown, float speed)
        {
            while (true)
            {
                yield return new WaitForSeconds(cooldown);
                var lane = waterAreaCharacterController.GetRandomLane();
                SpawnNewObstacle(lane, speed);
            }
        }
    }
}