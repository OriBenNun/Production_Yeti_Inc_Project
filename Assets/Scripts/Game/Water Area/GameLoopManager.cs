using System.Collections;
using Game.Water_Area.Obstacles;
using UnityEngine;

namespace Game.Water_Area
{
    public class GameLoopManager : MonoBehaviour
    {
        [SerializeField] private Canvas gameOverCanvas;
        [SerializeField] private Canvas pauseCanvas;

        [SerializeField] private ObstaclesManager obstaclesManager;
        [SerializeField] private IceChoppingIslandsManager iceChoppingIslandsManager;
        [SerializeField] private float timeUntilObstalcesStop = 5f;
        [SerializeField] private float timeUntilIceChoppingIslandStarts = 8f;


        private float _timeFromStart;
        
        private void Awake()
        {
            gameOverCanvas.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
            
            WaterPlayer.OnPlayerDied += WaterPlayerOnPlayerDied;
            IceChoppingIslandsManager.OnPlayerReachedIceChoppingIsland += HandleOnPlayerReachedIceChoppingIsland;
        }

        private void Start()
        {
            StartCoroutine(StartGameSequence());
        }

        private IEnumerator StartGameSequence()
        {
            obstaclesManager.StartSpawning();
            
            yield return new WaitForSeconds(timeUntilObstalcesStop);
            
            obstaclesManager.StopSpawning();
            
            yield return new WaitForSeconds(timeUntilIceChoppingIslandStarts);
            
            iceChoppingIslandsManager.SpawnIsland();
        }

        private void OnDestroy()
        {
            Time.timeScale = 1.0f;
            WaterPlayer.OnPlayerDied -= WaterPlayerOnPlayerDied;
            IceChoppingIslandsManager.OnPlayerReachedIceChoppingIsland -= HandleOnPlayerReachedIceChoppingIsland;
        }

        public void ReloadGameScene()
        {
            Time.timeScale = 1.0f;
            SceneTransitionHandler.LoadGameSceneAsync();
        }
        
        public void LoadMetaScene()
        {
            Time.timeScale = 1.0f;
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
        
        private void HandleOnPlayerReachedIceChoppingIsland()
        {
            LoadIcePickingScene();
        }
        
        private void LoadIcePickingScene()
        {
            Time.timeScale = 1.0f;
            SceneTransitionHandler.LoadIcePickingSceneAsync();
        }

        private void WaterPlayerOnPlayerDied()
        {
            Time.timeScale = 0;
            gameOverCanvas.gameObject.SetActive(true);
        }
        
    }
}
