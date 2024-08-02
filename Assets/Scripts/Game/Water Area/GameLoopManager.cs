using UnityEngine;

namespace Game.Water_Area
{
    public class GameLoopManager : MonoBehaviour
    {
        [SerializeField] private Canvas gameOverCanvas;
        [SerializeField] private Canvas pauseCanvas;

        private void Awake()
        {
            gameOverCanvas.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
            WaterPlayer.OnPlayerDied += WaterPlayerOnPlayerDied;
        }
        
        private void OnDestroy()
        {
            Time.timeScale = 1.0f;
            WaterPlayer.OnPlayerDied -= WaterPlayerOnPlayerDied;
        }

        public void ReloadGameScene()
        {
            Time.timeScale = 1.0f;
            SceneTransitionHandler.LoadGameSceneAsync();
        }

        public void LoadIcePickingScene()
        {
            Time.timeScale = 1.0f;
            SceneTransitionHandler.LoadIcePickingSceneAsync();
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

        private void WaterPlayerOnPlayerDied()
        {
            Time.timeScale = 0;
            gameOverCanvas.gameObject.SetActive(true);
        }
        
    }
}
