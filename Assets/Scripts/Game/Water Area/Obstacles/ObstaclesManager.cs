using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Water_Area.Obstacles
{
    public class ObstaclesManager : MonoBehaviour
    {
        [SerializeField] private float initialSpawnCooldown = 0.5f;
        [SerializeField] private ObstaclesPoolSpawner obstaclesPoolSpawner;
        [SerializeField] private List<Sprite> obstaclesSprites;

        public void StartSpawning()
        {
            obstaclesPoolSpawner.StartSpawningRandomObstacles(initialSpawnCooldown, obstaclesSprites);
        }
        
        public void StopSpawning()
        {
            obstaclesPoolSpawner.StopSpawning();
        }
    }
}
