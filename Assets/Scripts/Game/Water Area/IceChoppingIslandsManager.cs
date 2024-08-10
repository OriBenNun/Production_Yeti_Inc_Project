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

        private IceChoppingIsland _instantiatedIsland;
        
        public static event Action<IceChoppingIsland> OnIceChoppingIslandStopped;

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
            _instantiatedIsland = Instantiate(islandPrefab, laneToSpawn.GetObstacleSpawnPosition(), Quaternion.identity);
            _instantiatedIsland.Init(islandSpeed, yPositionToStop, laneToSpawn);
        }
        
        private void OnIslandStopped(WaterAreaStopIsland island)
        {
            if (island is not IceChoppingIsland choppingIsland)
            {
                return;
            }
            
            OnIceChoppingIslandStopped?.Invoke(choppingIsland);
        }
    }
}
