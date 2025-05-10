using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static int PlayerCount = 1; // 1 = Singleplayer, 2 = Multiplayer

    [Header("UI References")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject playerSelectionPanel;

    void Start()
    {
        mainMenuPanel.SetActive(true);
        playerSelectionPanel.SetActive(false);
    }

    public void ShowPlayerSelection()
    {
        mainMenuPanel.SetActive(false);
        playerSelectionPanel.SetActive(true);
    }
    void BackToMainMenu()
    {
        playerSelectionPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
    public void StartGame(int players)
    {
        PlayerCount = players;
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}