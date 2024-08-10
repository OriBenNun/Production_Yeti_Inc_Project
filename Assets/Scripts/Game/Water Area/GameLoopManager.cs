using System.Collections;
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
        [SerializeField] private IceChoppingIslandsManager iceChoppingIslandsManager;
        [SerializeField] private float timeUntilObstaclesStop = 5f;
        [SerializeField] private float timeBetweenObstaclesStopAndChoppingIslandSpawn = 1f;
        
        [SerializeField] private WaterAreaCharacterController waterAreaCharacterController;
        [SerializeField] private WaterPlayer waterPlayer;
        
        // private float _timeFromStart;
        
        private static int _iceChoppingIslandStops = 0;
        
        private void Awake()
        {
            gameOverCanvas.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
            
            WaterPlayer.OnPlayerDied += WaterPlayerOnPlayerDied;
            WaterPlayer.OnPlayerReachedIsland += HandleOnPlayerReachedIsland;
            IceChoppingIslandsManager.OnIceChoppingIslandStopped += HandleOnIceChoppingIslandStopped;
        }

        private void Start()
        {
            StartCoroutine(StartGameSequence());
        }

        private void OnDestroy()
        {
            Time.timeScale = 1.0f;
            WaterPlayer.OnPlayerDied -= WaterPlayerOnPlayerDied;
            WaterPlayer.OnPlayerReachedIsland -= HandleOnPlayerReachedIsland;
            IceChoppingIslandsManager.OnIceChoppingIslandStopped -= HandleOnIceChoppingIslandStopped;
        }

        public void ReloadGameScene()
        {
            Time.timeScale = 1.0f;
            _iceChoppingIslandStops = 0;
            SceneTransitionHandler.LoadGameSceneAsync();
        }
        
        public void LoadMetaScene()
        {
            Time.timeScale = 1.0f;
            _iceChoppingIslandStops = 0;
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
            obstaclesManager.StartSpawning();

            var timeToWait = Mathf.Max((_iceChoppingIslandStops + 1) / 2f * timeUntilObstaclesStop, timeUntilObstaclesStop);
            yield return new WaitForSeconds(timeToWait);
            
            obstaclesManager.StopSpawning();
            
            yield return new WaitForSeconds(timeBetweenObstaclesStopAndChoppingIslandSpawn);
            
            iceChoppingIslandsManager.SpawnIsland();
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
