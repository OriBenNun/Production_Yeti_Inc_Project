using System;
using UnityEngine;

namespace Game.Water_Area
{
    public class IceChoppingIslandsManager : MonoBehaviour
    {
        [SerializeField] private WaterAreaCharacterController waterAreaCharacterController;
        [SerializeField] private IceChoppingIsland islandPrefab;
        [SerializeField] private float yPositionToStop = 4f;
        [SerializeField] private float islandSpeed = 1f;

        private IceChoppingIsland _instansiatedIsland;
        
        public static event Action OnPlayerReachedIceChoppingIsland;

        private void Awake()
        {
            WaterAreaStopIsland.OnIslandStopped += OnIslandStopped;
        }

        private void OnDestroy()
        {
            WaterAreaStopIsland.OnIslandStopped -= OnIslandStopped;
        }

        public void SpawnIsland()
        {
            var laneToSpawn = waterAreaCharacterController.GetRandomEdgeLane();
            _instansiatedIsland = Instantiate(islandPrefab, laneToSpawn.GetObstacleSpawnPosition(), Quaternion.identity);
            _instansiatedIsland.Init(islandSpeed, yPositionToStop);
            // island.transform.SetParent(laneToSpawn.transform);
        }
        
        private void OnIslandStopped(WaterAreaStopIsland island)
        {
            if (island is not IceChoppingIsland)
            {
                return;
            }
            OnPlayerReachedIceChoppingIsland?.Invoke();
        }
    }
}
