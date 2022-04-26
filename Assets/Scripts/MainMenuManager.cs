using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void LoadScene(string scenename)
    {
        if (scenename != "GameOver")
        {
            LoadedLevel.LoadedLevelString = scenename;
        }
        SceneManager.LoadScene(scenename);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(LoadedLevel.LoadedLevelString);
    }
}