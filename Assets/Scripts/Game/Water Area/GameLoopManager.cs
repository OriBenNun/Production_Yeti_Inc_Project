using System;
using System.Collections;
using System.Globalization;
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
        [SerializeField] private float timeUntilObstaclesStopAndIslandSpawn = 30f;
        [FormerlySerializedAs("timeBetweenObstaclesStopAndChoppingIslandSpawn")] [SerializeField] private float timeBetweenObstaclesStopAndIslandSpawn = 1f;
        [SerializeField] private float initialObstacleSpeed = 2.6f;
        [SerializeField] private float initialSpawnCooldown = 1.4f;
        [SerializeField] private float minimumSpawnCooldown = 0.4f;
        [SerializeField] private int initialDistancePerSecond = 71;
        [SerializeField] private float distancePerSecondMultiplierPerIceChoppingIslandStop = 0.5f;
        [SerializeField] private float obstacleSpeedMultiplierPerIceChoppingIslandStop = 0.5f;
        [SerializeField] private float spawnCooldownMultiplierPerIceChoppingIslandStop = 0.2f;

        [SerializeField] private WaterAreaCharacterController waterAreaCharacterController;
        [SerializeField] private WaterPlayer waterPlayer;

        public static event Action OnGameQuited;
        // private float _timeFromStart;
        
        private static int _islandStops;

        private static bool _isPreviousIslandIceChopping;
        
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
            StopIslandsManager.OnIslandReachedDestination += HandleOnIslandReachedDestination;
            
            OnGameQuited += HandleOnGameQuited;
        }

        private void Start()
        {
            StartCoroutine(StartGameSequence());
        }

        private void Update()
        {
            if (Time.timeScale == 0) { return; }
            
            CurrentRunDataHandler.DistanceTraveled += initialDistancePerSecond +
                                Mathf.CeilToInt(_islandStops * distancePerSecondMultiplierPerIceChoppingIslandStop * Time.deltaTime);
        }

        private void OnDestroy()
        {
            Time.timeScale = 1.0f;
            WaterPlayer.OnPlayerDied -= WaterPlayerOnPlayerDied;
            WaterPlayer.OnPlayerReachedIsland -= HandleOnPlayerReachedIsland;
            StopIslandsManager.OnIslandReachedDestination -= HandleOnIslandReachedDestination;
            
            OnGameQuited -= CurrentRunDataHandler.Reset;
        }

        public void ReloadGameScene()
        {
            Time.timeScale = 1.0f;
            _islandStops = 0;
            _isPreviousIslandIceChopping = false;
            OnGameQuited?.Invoke();
            SceneTransitionHandler.LoadGameSceneAsync();
        }
        
        public void LoadMetaScene()
        {
            Time.timeScale = 1.0f;
            _islandStops = 0;
            _isPreviousIslandIceChopping = false;
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
            var speed = initialObstacleSpeed + _islandStops * obstacleSpeedMultiplierPerIceChoppingIslandStop;
            var spawnCooldown = Mathf.Max(minimumSpawnCooldown,initialSpawnCooldown - _islandStops * spawnCooldownMultiplierPerIceChoppingIslandStop);
            obstaclesManager.StartSpawning(spawnCooldown, speed);

            yield return new WaitForSeconds(timeUntilObstaclesStopAndIslandSpawn);
            
            obstaclesManager.StopSpawning();
            
            yield return new WaitForSeconds(timeBetweenObstaclesStopAndIslandSpawn);
            
            SpawnIsland();
        }

        private void SpawnIsland()
        {
            if (_isPreviousIslandIceChopping)
            {
                stopIslandsManager.SpawnShopIsland();
                _isPreviousIslandIceChopping = false;
                return;
            }
            
            stopIslandsManager.SpawnIceChoppingIsland();
            _isPreviousIslandIceChopping = true;
        }

        private void HandleOnIslandReachedDestination(WaterAreaStopIsland waterAreaStopIsland)
        {
            StartMovePlayerToStoppedIsland(waterAreaStopIsland);
        }

        private void StartMovePlayerToStoppedIsland(WaterAreaStopIsland island)
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
            _islandStops++;
            SceneTransitionHandler.LoadIcePickingSceneAsync();
        }

        private void WaterPlayerOnPlayerDied()
        {
            Time.timeScale = 0;
            gameOverCanvas.gameObject.SetActive(true);
        }

        private void SaveCurrentRunData()
        {
            var cubesCollected = CurrentRunDataHandler.TotalCurrencyInRun;

            if (!PlayerPrefs.HasKey("FirstCubes"))
            {
                PlayerPrefs.SetString("FirstDate", DateTime.Today.ToString("dd/MM/yyyy"));
                PlayerPrefs.SetInt("FirstCubes", cubesCollected);
                PlayerPrefs.SetInt("FirstDistance", CurrentRunDataHandler.DistanceTraveled);
            }
            else if (cubesCollected > PlayerPrefs.GetInt("FirstCubes"))
            {
                if (PlayerPrefs.HasKey("SecondCubes"))
                {
                    if (PlayerPrefs.HasKey("ThirdCubes"))
                    {
                        PlayerPrefs.SetString("ThirdDate", PlayerPrefs.GetString("SecondDate"));
                        PlayerPrefs.SetInt("ThirdCubes", PlayerPrefs.GetInt("SecondCubes"));
                        PlayerPrefs.SetInt("ThirdDistance", PlayerPrefs.GetInt("SecondDistance"));
                    }
                    
                    PlayerPrefs.SetString("SecondDate", PlayerPrefs.GetString("FirstDate"));
                    PlayerPrefs.SetInt("SecondCubes", PlayerPrefs.GetInt("FirstCubes"));
                    PlayerPrefs.SetInt("SecondDistance", PlayerPrefs.GetInt("FirstDistance"));
                    
                    PlayerPrefs.SetString("FirstDate", DateTime.Today.ToString("dd/MM/yyyy"));
                    PlayerPrefs.SetInt("FirstCubes", cubesCollected);
                    PlayerPrefs.SetInt("FirstDistance", CurrentRunDataHandler.DistanceTraveled);
                }
                else if (!PlayerPrefs.HasKey("SecondCubes"))
                {
                    PlayerPrefs.SetString("SecondDate", PlayerPrefs.GetString("FirstDate"));
                    PlayerPrefs.SetInt("SecondCubes", PlayerPrefs.GetInt("FirstCubes"));
                    PlayerPrefs.SetInt("SecondDistance", PlayerPrefs.GetInt("FirstDistance"));
                    
                    PlayerPrefs.SetString("FirstDate", DateTime.Today.ToString("dd/MM/yyyy"));
                    PlayerPrefs.SetInt("FirstCubes", cubesCollected);
                    PlayerPrefs.SetInt("FirstDistance", CurrentRunDataHandler.DistanceTraveled);
                }
                else if (cubesCollected > PlayerPrefs.GetInt("SecondCubes"))
                {
                    if (!PlayerPrefs.HasKey("ThirdCubes"))
                    {
                        PlayerPrefs.SetString("ThirdDate", PlayerPrefs.GetString("SecondDate"));
                        PlayerPrefs.SetInt("ThirdCubes", PlayerPrefs.GetInt("SecondCubes"));
                        PlayerPrefs.SetInt("ThirdDistance", PlayerPrefs.GetInt("SecondDistance"));
                        
                        PlayerPrefs.SetString("SecondDate", DateTime.Today.ToString("dd/MM/yyyy"));
                        PlayerPrefs.SetInt("SecondCubes", cubesCollected);
                        PlayerPrefs.SetInt("SecondDistance", CurrentRunDataHandler.DistanceTraveled);
                    }
                    else if (cubesCollected > PlayerPrefs.GetInt("ThirdCubes"))
                    {
                        PlayerPrefs.SetString("ThirdDate", DateTime.Today.ToString("dd/MM/yyyy"));
                        PlayerPrefs.SetInt("ThirdCubes", cubesCollected);
                        PlayerPrefs.SetInt("ThirdDistance", CurrentRunDataHandler.DistanceTraveled);
                    }
                }
                else if (!PlayerPrefs.HasKey("ThirdCubes"))
                {
                    PlayerPrefs.SetString("ThirdDate", DateTime.Today.ToString("dd/MM/yyyy"));
                    PlayerPrefs.SetInt("ThirdCubes", cubesCollected);
                    PlayerPrefs.SetInt("ThirdDistance", CurrentRunDataHandler.DistanceTraveled);
                }
            }
            else if (!PlayerPrefs.HasKey("SecondCubes"))
            {
                PlayerPrefs.SetString("SecondDate", DateTime.Today.ToString("dd/MM/yyyy"));
                PlayerPrefs.SetInt("SecondCubes", cubesCollected);
                PlayerPrefs.SetInt("SecondDistance", CurrentRunDataHandler.DistanceTraveled);
            }
            
            PlayerPrefs.Save();
        }

        private void HandleOnPlayerReachedIsland(WaterAreaStopIsland island)
        {
            if (island is IceChoppingIsland)
            {
                // TODO add yeti animation
                LoadIcePickingScene();
            }
            else if (island is ShopIsland)
            {
                // TODO add yeti animation
                LoadShopScene();
            }
        }

        private void LoadShopScene()
        {
            Time.timeScale = 1.0f;
            _islandStops++;
            SceneTransitionHandler.LoadShopSceneAsync();
        }
        
        private void HandleOnGameQuited()
        {
            SaveCurrentRunData();
            CurrentRunDataHandler.Reset();
        }
    }
}
