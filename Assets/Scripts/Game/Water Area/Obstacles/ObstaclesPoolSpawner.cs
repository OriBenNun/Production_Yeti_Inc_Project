using System.Collections;
using Pool_System;
using UnityEngine;

namespace Game.Water_Area.Obstacles
{
    public class ObstaclesPoolSpawner : ObjectPool<Obstacle>
    {
        [SerializeField] private Obstacle obstaclePrefab;
        [SerializeField] private int initialPoolSize = 50;
        [SerializeField] private WaterAreaCharacterController waterAreaCharacterController;

        private void Awake()
        {
            InitPool(initialPoolSize, obstaclePrefab, transform);
        }
        
        public void StartSpawningRandomObstacles(float spawnCooldown)
        {
            StartCoroutine(SpawnRandomObstaclesWithCooldown(spawnCooldown));
        }

        private Obstacle SpawnNewObstacle(LaneManager lane)
        {
            var obstacle = GetReadyObject();
            obstacle.SetPosition(lane.GetObstacleSpawnPosition());
            obstacle.gameObject.SetActive(true);
            obstacle.Init(10);
            return obstacle;
        }

        private IEnumerator SpawnRandomObstaclesWithCooldown(float cooldown)
        {
            while (true)
            {
                yield return new WaitForSeconds(cooldown);
                var lane = waterAreaCharacterController.GetRandomLane();
                SpawnNewObstacle(lane);
            }
        }
    }
}
