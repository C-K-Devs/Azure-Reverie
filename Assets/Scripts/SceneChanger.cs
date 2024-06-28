using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor, so we need to simulate a quit.
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // This will quit the game when running the built application.
            Application.Quit();
#endif
    }
}
