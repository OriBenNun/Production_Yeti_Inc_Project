using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject leadersPanel;

    public void SettingsPanel()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void LeadersboardPanel()
    {
        leadersPanel.SetActive(!leadersPanel.activeSelf);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("MainGameLoop_Scene");
    }
}
