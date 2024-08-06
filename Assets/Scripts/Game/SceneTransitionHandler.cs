using UnityEngine.SceneManagement;

namespace Game
{
    public static class SceneTransitionHandler
    {
        private const int _metaSceneIndex = 0;
        private const int _gameSceneIndex = 1;
        private const int _icePickingSceneIndex = 2;

        public static void LoadGameSceneAsync()
        {
            LoadSceneAsync(_gameSceneIndex);
        }
    
        public static void LoadIcePickingSceneAsync()
        {
            LoadSceneAsync(_icePickingSceneIndex);
        }
        
        public static void LoadMetaSceneAsync()
        {
            LoadSceneAsync(_metaSceneIndex);
        }
        
        private static void LoadSceneAsync(int index)
        {
            SceneManager.LoadSceneAsync(index);
        }

    }
}
