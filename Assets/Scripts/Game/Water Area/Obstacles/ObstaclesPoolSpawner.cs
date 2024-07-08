using System;
using Pool_System;
using UnityEngine;

namespace Game.Water_Area.Obstacles
{
    public class ObstaclesPoolSpawner : ObjectPool<Obstacle>
    {
        [SerializeField] private Obstacle obstaclePrefab;
        [SerializeField] private int initialPoolSize = 50;

        private void Awake()
        {
            InitPool(initialPoolSize, obstaclePrefab, transform);
        }

        private void Start()
        {
            
        }
    }
}
