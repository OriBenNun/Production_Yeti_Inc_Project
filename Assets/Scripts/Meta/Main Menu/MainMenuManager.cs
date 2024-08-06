using Game;
using UnityEngine;

namespace Meta.Main_Menu
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] GameObject settingsPanel;
        [SerializeField] GameObject leadersPanel;

        public void StartGame()
        {
            SceneTransitionHandler.LoadGameSceneAsync();
        }

        public void SettingsPanel()
        {
            settingsPanel.SetActive(!settingsPanel.activeSelf);
        }

        public void LeadersboardPanel()
        {
            leadersPanel.SetActive(!leadersPanel.activeSelf);
        }
    }
}
