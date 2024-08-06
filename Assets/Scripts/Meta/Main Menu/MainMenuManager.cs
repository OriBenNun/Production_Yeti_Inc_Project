using Game;
using UnityEngine;

namespace Meta.Main_Menu
{
    public class MainMenuManager : MonoBehaviour
    {
        public void StartGame()
        {
            SceneTransitionHandler.LoadGameSceneAsync();
        }
    }
}
