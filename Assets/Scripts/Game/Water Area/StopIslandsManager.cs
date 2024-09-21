using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Water_Area
{
    public class StopIslandsManager : MonoBehaviour
    {
        [SerializeField] private WaterAreaCharacterController waterAreaCharacterController;
        [FormerlySerializedAs("islandPrefab")] [SerializeField] private IceChoppingIsland iceChoppingIslandPrefab;
        [SerializeField] private ShopIsland shopIslandPrefab;
        [SerializeField] private float yPositionToStop = 4f;
        [SerializeField] private float islandSpeed = 1f;
        
        public static event Action<WaterAreaStopIsland> OnIslandReachedDestination;

        private void Awake()
        {
            WaterAreaStopIsland.OnIslandStopped += OnIslandStopped;
        }

        private void OnDestroy()
        {
            WaterAreaStopIsland.OnIslandStopped -= OnIslandStopped;
        }

        public void SpawnIceChoppingIsland()
        {
            var laneToSpawn = waterAreaCharacterController.GetRandomEdgeLane();
            var island = Instantiate(iceChoppingIslandPrefab, laneToSpawn.GetObstacleSpawnPosition(), Quaternion.identity);
            island.Init(islandSpeed, yPositionToStop);
        }
        
        public void SpawnShopIsland()
        {
            var laneToSpawn = waterAreaCharacterController.GetRandomEdgeLane();
            var island = Instantiate(shopIslandPrefab, laneToSpawn.GetObstacleSpawnPosition(), Quaternion.identity);
            island.Init(islandSpeed, yPositionToStop);
        }
        
        private void OnIslandStopped(WaterAreaStopIsland island)
        {
            OnIslandReachedDestination?.Invoke(island);
        }
    }
}
