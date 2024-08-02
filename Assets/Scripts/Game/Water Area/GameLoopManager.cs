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
            
            WaterPlayer.OnPlayerDied += WaterPlayerOnOnPlayerDied;
        }
        
        private void OnDestroy()
        {
            WaterPlayer.OnPlayerDied -= WaterPlayerOnOnPlayerDied;
        }

        public void ReloadGameScene()
        {
            Time.timeScale = 1.0f;
            SceneTransitionHandler.LoadGameSceneAsync();
        }

        public void LoadIcePickingScene()
        {
            SceneTransitionHandler.LoadIcePickingSceneAsync();
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

        private void WaterPlayerOnOnPlayerDied()
        {
            Time.timeScale = 0;
            gameOverCanvas.gameObject.SetActive(true);
        }
        
    }
}
