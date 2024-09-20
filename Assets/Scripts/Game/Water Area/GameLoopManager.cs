using System;
using System.Collections;
using Game.Shop;
using Game.Water_Area.Obstacles;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Water_Area
{
    public class GameLoopManager : MonoBehaviour
    {
        [SerializeField] private Canvas gameOverCanvas;
        [SerializeField] private Canvas pauseCanvas;

        [SerializeField] private ObstaclesManager obstaclesManager;
        [FormerlySerializedAs("iceChoppingIslandsManager")] [SerializeField] private StopIslandsManager stopIslandsManager;
        [SerializeField] private float timeUntilObstaclesStop = 5f;
        [SerializeField] private float timeBetweenObstaclesStopAndChoppingIslandSpawn = 1f;
        [SerializeField] private float initialObstacleSpeed = 2.6f;
        [SerializeField] private float initialSpawnCooldown = 1.4f;
        [SerializeField] private float minimumSpawnCooldown = 0.4f;
        [SerializeField] private int initialDistancePerSecond = 71;
        [SerializeField] private float distancePerSecondMultiplierPerIceChoppingIslandStop = 0.5f;
        [SerializeField] private float obstacleSpeedMultiplierPerIceChoppingIslandStop = 0.5f;
        [SerializeField] private float timeBetweenMultiplierPerIceChoppingIslandStop = 0.5f;
        [SerializeField] private float spawnCooldownMultiplierPerIceChoppingIslandStop = 0.2f;

        [SerializeField] private WaterAreaCharacterController waterAreaCharacterController;
        [SerializeField] private WaterPlayer waterPlayer;

        public static event Action OnGameQuited;
        // private float _timeFromStart;
        
        private static int _iceChoppingIslandStops = 0;
        
        private void Awake()
        {
            if (!CurrentRunDataHandler.HasInitialized)
            {
                CurrentRunDataHandler.Init();
            }

            gameOverCanvas.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
            
            WaterPlayer.OnPlayerDied += WaterPlayerOnPlayerDied;
            WaterPlayer.OnPlayerReachedIsland += HandleOnPlayerReachedIsland;
            StopIslandsManager.OnIceChoppingIslandStopped += HandleOnIceChoppingIslandStopped;
            
            OnGameQuited += CurrentRunDataHandler.Reset;
        }

        private void Start()
        {
            StartCoroutine(StartGameSequence());
        }

        private void Update()
        {
            if (Time.timeScale == 0) { return; }
            
            CurrentRunDataHandler.DistanceTraveled += initialDistancePerSecond +
                                Mathf.CeilToInt(_iceChoppingIslandStops * distancePerSecondMultiplierPerIceChoppingIslandStop * Time.deltaTime);
        }

        private void OnDestroy()
        {
            Time.timeScale = 1.0f;
            WaterPlayer.OnPlayerDied -= WaterPlayerOnPlayerDied;
            WaterPlayer.OnPlayerReachedIsland -= HandleOnPlayerReachedIsland;
            StopIslandsManager.OnIceChoppingIslandStopped -= HandleOnIceChoppingIslandStopped;
            
            OnGameQuited -= CurrentRunDataHandler.Reset;
        }

        public void ReloadGameScene()
        {
            Time.timeScale = 1.0f;
            _iceChoppingIslandStops = 0;
            OnGameQuited?.Invoke();
            SceneTransitionHandler.LoadGameSceneAsync();
        }
        
        public void LoadMetaScene()
        {
            Time.timeScale = 1.0f;
            _iceChoppingIslandStops = 0;
            OnGameQuited?.Invoke();
            SceneTransitionHandler.LoadMetaSceneAsync();
        }
        
        public void PauseGame()
        {
            Time.timeScale = 0;
            pauseCanvas.gameObject.SetActive(true);
        }
        
        public void ResumeGame()
        {
            Time.timeScale = 1.0f;
            pauseCanvas.gameObject.SetActive(false);
        }
        
        private IEnumerator StartGameSequence()
        {
            var speed = initialObstacleSpeed + _iceChoppingIslandStops * obstacleSpeedMultiplierPerIceChoppingIslandStop;
            var spawnCooldown = Mathf.Max(minimumSpawnCooldown,initialSpawnCooldown - _iceChoppingIslandStops * spawnCooldownMultiplierPerIceChoppingIslandStop);
            obstaclesManager.StartSpawning(spawnCooldown, speed);

            var timeToWait = timeUntilObstaclesStop + _iceChoppingIslandStops * timeBetweenMultiplierPerIceChoppingIslandStop;
            yield return new WaitForSeconds(timeToWait);
            
            obstaclesManager.StopSpawning();
            
            yield return new WaitForSeconds(timeBetweenObstaclesStopAndChoppingIslandSpawn);
            
            stopIslandsManager.SpawnIsland();
        }
        
        private void HandleOnIceChoppingIslandStopped(IceChoppingIsland island)
        {
            StartMovePlayerToStoppedIsland(island);
        }

        private void StartMovePlayerToStoppedIsland(IceChoppingIsland island)
        {
            DisableControls();
            waterPlayer.StartMoveToIsland(island);
        }

        private void DisableControls()
        {
            waterAreaCharacterController.DisableControls();
        }

        private void LoadIcePickingScene()
        {
            Time.timeScale = 1.0f;
            _iceChoppingIslandStops++;
            SceneTransitionHandler.LoadIcePickingSceneAsync();
        }

        private void WaterPlayerOnPlayerDied()
        {
            Time.timeScale = 0;
            gameOverCanvas.gameObject.SetActive(true);
        }
        
        private void HandleOnPlayerReachedIsland(WaterAreaStopIsland island)
        {
            if (island is IceChoppingIsland)
            {
                // TODO add yeti animation
                LoadIcePickingScene();
            }
        }
        
    }
}
