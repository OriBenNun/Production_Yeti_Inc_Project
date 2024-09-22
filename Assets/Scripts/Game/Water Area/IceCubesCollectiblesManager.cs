using System.Collections;
using UnityEngine;

namespace Game.Water_Area
{
    public class IceCubesCollectiblesManager : MonoBehaviour
    {
        [SerializeField] private CollectibleIceCube iceCubePrefab;
        [SerializeField] private WaterAreaCharacterController waterAreaCharacterController;
        [SerializeField] private float timeBetweenObstacleAndIceCubeSpawn = 0.3f;


        public const int ChanceToSpawnIceCube = 40;

        public void SpawnIceCube(float speed)
        {
            StartCoroutine(SpawnIceCubeWithDelay(speed));
        }

        private IEnumerator SpawnIceCubeWithDelay(float speed)
        {
            yield return new WaitForSeconds(timeBetweenObstacleAndIceCubeSpawn);
            
            var lane = waterAreaCharacterController.GetRandomLane();
            var iceCube = Instantiate(iceCubePrefab);
            iceCube.Init(10, speed, lane.GetObstacleSpawnPosition());
        }
    }
}