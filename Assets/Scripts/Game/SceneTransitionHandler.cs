using UnityEngine.SceneManagement;

namespace Game
{
    public static class SceneTransitionHandler
    {
        private const int _gameSceneIndex = 0;
        private const int _icePickingSceneIndex = 1;

        public static void LoadGameSceneAsync()
        {
            LoadSceneAsync(_gameSceneIndex);
        }
    
        public static void LoadIcePickingSceneAsync()
        {
            LoadSceneAsync(_icePickingSceneIndex);
        }
    
        private static void LoadSceneAsync(int index)
        {
            SceneManager.LoadSceneAsync(index);
        }
    }
}
