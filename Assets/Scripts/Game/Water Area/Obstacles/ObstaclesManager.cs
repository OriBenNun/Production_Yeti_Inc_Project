using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Water_Area.Obstacles
{
    public class ObstaclesManager : MonoBehaviour
    {
        [SerializeField] private ObstaclesPoolSpawner obstaclesPoolSpawner;
        [SerializeField] private List<Sprite> obstaclesSprites;

        public void StartSpawning(float spawnCooldown, float speed)
        {
            obstaclesPoolSpawner.StartSpawningRandomObstacles(spawnCooldown, obstaclesSprites, speed);
        }

        public void StopSpawning()
        {
            obstaclesPoolSpawner.StopSpawning();
        }
    }
}