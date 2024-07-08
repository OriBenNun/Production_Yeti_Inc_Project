using System;
using UnityEngine;

namespace Game.Water_Area.Obstacles
{
    public class ObstaclesManager : MonoBehaviour
    {
        [SerializeField] private float initialSpawnCooldown = 0.5f;
        [SerializeField] private ObstaclesPoolSpawner obstaclesPoolSpawner;

        private void Start()
        {
            obstaclesPoolSpawner.StartSpawningRandomObstacles(initialSpawnCooldown);
        }
    }
}
