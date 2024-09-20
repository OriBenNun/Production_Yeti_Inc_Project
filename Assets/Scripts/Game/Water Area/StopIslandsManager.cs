using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Water_Area
{
    public class StopIslandsManager : MonoBehaviour
    {
        [SerializeField] private WaterAreaCharacterController waterAreaCharacterController;
        [FormerlySerializedAs("islandPrefab")] [SerializeField] private IceChoppingIsland iceChoppingIslandPrefab;
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
            _instantiatedIsland = Instantiate(iceChoppingIslandPrefab, laneToSpawn.GetObstacleSpawnPosition(), Quaternion.identity);
            _instantiatedIsland.Init(islandSpeed, yPositionToStop);
        }
        
        private void OnIslandStopped(WaterAreaStopIsland island)
        {
            if (island is IceChoppingIsland choppingIsland)
            {
                OnIceChoppingIslandStopped?.Invoke(choppingIsland);
            }
        }
    }
}
